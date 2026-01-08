using Industry.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Industry.Tiles
{
    public class BronzeFurnaceEntity : ModTileEntity
    {
        private BronzeFurnaceRecipe currentRecipe;
        private int craftProgress;

        public override void Update()
        {
            if (!CheckMultiblock(Position.X, Position.Y, out _))
            {
                Kill(Position.X, Position.Y);
                return;
            }
            TryCraft();
        }

        #region Craft Logic

        private void TryCraft()
        {

            if (!TryGetChests(out Chest input, out Chest output))
            {
                ResetCraft();
                return;
            }

            BronzeFurnaceRecipe recipe = FindRecipe(input, output);

            if (recipe == null)
            {
                ResetCraft();
                return;
            }

            if (currentRecipe != recipe)
            {
                currentRecipe = recipe;
                craftProgress = 0;
            }

            craftProgress++;

            if (craftProgress >= recipe.time)
            {
                recipe.ConsumeInput(input);
                recipe.ProduceOutput(output);

                craftProgress = 0;

                NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, input.x, input.y);
                NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, output.x, output.y);
            }
        }

        private BronzeFurnaceRecipe FindRecipe(Chest input, Chest output)
        {
            foreach (var recipe in BronzeFurnaceRecipes.Recipes)
            {
                if (!recipe.CanCraft(input))
                    continue;

                if (!recipe.CanOutputFit(output))
                    continue;

                return recipe;
            }

            return null;
        }

        private void ResetCraft()
        {
            currentRecipe = null;
            craftProgress = 0;
        }

        #endregion

        #region Multiblock & Chests

        private bool TryGetChests(out Chest input, out Chest output)
        {
            input = null;
            output = null;

            int i = Position.X;
            int j = Position.Y;

            int inIndex = Chest.FindChest(i - 2, j - 3);
            int outIndex = Chest.FindChest(i + 1, j - 3);

            if (inIndex < 0 || outIndex < 0)
                return false;

            input = Main.chest[inIndex];
            output = Main.chest[outIndex];
            return true;
        }

        private bool CheckMultiblock(int i, int j, out string error)
        {
            error = null;

            if (!IsChestAt(i - 2, j - 3))
            {
                error = "Нет входного сундука";
                return false;
            }

            if (!IsChestAt(i + 1, j - 3))
            {
                error = "Нет выходного сундука";
                return false;
            }

            Point[] bricks =
            {
                new(i - 1, j),
                new(i + 1, j),
                new(i - 2, j - 1),
                new(i - 1, j - 1),
                new(i,     j - 1),
                new(i + 1, j - 1),
                new(i + 2, j - 1),
            };

            foreach (var p in bricks)
            {
                Tile tile = Framing.GetTileSafely(p.X, p.Y);
                if (!tile.HasTile || tile.TileType != ModContent.TileType<BronzeBrick>())
                {
                    error = "Нарушен мультиблок";
                    return false;
                }
            }

            return true;
        }

        private bool IsChestAt(int x, int y)
        {
            int index = Chest.FindChest(x, y);
            if (index < 0)
                return false;

            return Main.tile[x, y].TileType == TileID.Containers;
        }

        public override bool IsTileValidForEntity(int x, int y)
            => Main.tile[x, y].HasTile &&
               Main.tile[x, y].TileType == ModContent.TileType<BronzeFurnace>();

        #endregion
    }

    // ===================== RECIPE =====================

    public class BronzeFurnaceRecipe
    {
        public Dictionary<int, int> Inputs = new();
        public int OutputItem;
        public int OutputStack = 1;
        public int time = 60; // В ТИКАХ

        public bool CanCraft(Chest chest)
        {
            foreach (var req in Inputs)
            {
                int count = 0;

                foreach (var item in chest.item)
                    if (item.type == req.Key)
                        count += item.stack;

                if (count < req.Value)
                    return false;
            }

            return true;
        }

        public bool CanOutputFit(Chest chest)
        {
            for (int i = 0; i < 40; i++)
            {
                Item item = chest.item[i];

                if (item.IsAir)
                    return true;

                if (item.type == OutputItem &&
                    item.stack + OutputStack <= item.maxStack)
                    return true;
            }

            return false;
        }

        public void ConsumeInput(Chest chest)
        {
            foreach (var req in Inputs)
            {
                int need = req.Value;

                for (int i = 0; i < 40 && need > 0; i++)
                {
                    Item item = chest.item[i];
                    if (item.type != req.Key)
                        continue;

                    int take = Math.Min(item.stack, need);
                    item.stack -= take;
                    need -= take;

                    if (item.stack <= 0)
                        chest.item[i].TurnToAir();
                }
            }
        }

        public void ProduceOutput(Chest chest)
        {
            for (int i = 0; i < 40; i++)
            {
                Item item = chest.item[i];

                if (item.IsAir)
                {
                    item.SetDefaults(OutputItem);
                    item.stack = OutputStack;
                    return;
                }

                if (item.type == OutputItem)
                {
                    item.stack += OutputStack;
                    return;
                }
            }
        }
    }

    // ===================== RECIPE LIST =====================

    public static class BronzeFurnaceRecipes
    {
        public static readonly List<BronzeFurnaceRecipe> Recipes = new()
        {
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.CopperBar] = 3,
                    [ItemID.TinBar] = 1
                },
                OutputItem = ModContent.ItemType<BronzeBar>(),
                OutputStack = 2,
                time = 180
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.PlatinumOre] = 4,
                },
                OutputItem = ItemID.PlatinumBar,
                OutputStack = 1,
                time = 300
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.GoldOre] = 4,
                },
                OutputItem = ItemID.GoldBar,
                OutputStack = 1,
                time = 300
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.DemoniteOre] = 3,
                },
                OutputItem = ItemID.DemoniteBar,
                OutputStack = 1,
                time = 360
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.CrimtaneOre] = 3,
                },
                OutputItem = ItemID.CrimtaneBar,
                OutputStack = 1,
                time = 360
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.Meteorite] = 3,
                },
                OutputItem = ItemID.MeteoriteBar,
                OutputStack = 1,
                time = 400
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.CopperOre] = 5,
                },
                OutputItem = ItemID.CopperBar,
                OutputStack = 2,
                time = 120
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.TinOre] = 5,
                },
                OutputItem = ItemID.TinBar,
                OutputStack = 2,
                time = 110
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.LeadOre] = 5,
                },
                OutputItem = ItemID.LeadBar,
                OutputStack = 2,
                time = 150
            },
            new BronzeFurnaceRecipe
            {
                Inputs =
                {
                    [ItemID.IronOre] = 5,
                },
                OutputItem = ItemID.IronBar,
                OutputStack = 2,
                time = 160
            },
        };
    }
}
