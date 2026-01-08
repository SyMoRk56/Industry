using Industry.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Industry.Tiles
{
    public class BronzeMixerEntity : ModTileEntity
    {
        int timer;
        public override void Update()
        {
            // если мультиблок сломан — убиваем TileEntity
            if (!CheckMultiblock(Position.X, Position.Y, out var e))
            {
                Main.NewText(e);
                Kill(Position.X, Position.Y);
                return;
            }

            timer++;

            if (timer >= 60) // 1 секунда
            {
                timer = 0;
                TryCraft();
            }
        }

        private bool CheckMultiblock(int i, int j, out string error)
        {
            error = null;

            // === ВХОДНОЙ СУНДУК ===
            if (!IsChestAt(i - 2, j - 3))
            {
                error = "Нет входного сундука слева сверху";
                return false;
            }

            // === ВЫХОДНОЙ СУНДУК ===
            if (!IsChestAt(i + 1, j - 3))
            {
                error = "Нет выходного сундука справа сверху";
                return false;
            }

            // === ПРОВЕРКА КИРПИЧЕЙ ===
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
                if (tile.TileType != ModContent.TileType<BronzeBrick>())
                {
                    error = $"Не хватает бронзового кирпича ({p.X - i:+#;-#;0}, {p.Y - j:+#;-#;0})";
                    return false;
                }
            }

            return true;
        }
        private bool IsChestAt(int x, int y)
        {
            int chestIndex = Chest.FindChest(x, y);
            if (chestIndex < 0)
                return false;

            Tile tile = Framing.GetTileSafely(x, y);
            return tile.TileType == TileID.Containers;
        }
        private Chest GetInputChest(int i, int j)
    => Main.chest[Chest.FindChest(i - 2, j - 3)];

        private Chest GetOutputChest(int i, int j)
            => Main.chest[Chest.FindChest(i + 1, j - 3)];

        private void TryCraft()
        {
            //Main.NewText("Bronze mixer entity: TryCraft" + TryGetChests(out Chest i, out Chest o).ToString() + " " + BronzeMixerRecipes.Recipes.Count, Color.Orange);
            if (!TryGetChests(out Chest input, out Chest output))
                return;

            foreach (var recipe in BronzeMixerRecipes.Recipes)
            {
                if (!recipe.CanCraft(input))
                    continue;
                if (!recipe.CanOutputFit(output))
                    continue;
                recipe.ConsumeInput(input);
                recipe.ProduceOutput(output);

                NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, input.x, input.y);
                NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, output.x, output.y);
                break;
            }
        }

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

        public override bool IsTileValidForEntity(int x, int y)
            => Main.tile[x, y].TileType == ModContent.TileType<BronzeMixerTile>();
    }
    public class BronzeMixerRecipe
    {
        public Dictionary<int, int> Inputs = new();
        public int OutputItem;
        public int OutputStack;

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
                    Main.NewText("Chest slot is air");
                    chest.item[i].SetDefaults(OutputItem);
                    chest.item[i].stack = OutputStack;
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


        public static class BronzeMixerRecipes
        {
            public static readonly List<BronzeMixerRecipe> Recipes = new()
        {
            new BronzeMixerRecipe
            {
                Inputs =
                {
                    [ItemID.CopperBar] = 3,
                    [ItemID.TinBar] = 1
                },
                OutputItem = ModContent.ItemType<BronzeBar>(),
                OutputStack = 2
                
            }
        };
        }
    
}