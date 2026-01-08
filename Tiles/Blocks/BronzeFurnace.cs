using Industry.Items;
using Industry.Items.Materials;
using Industry.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Industry.Tiles
{
    public class BronzeFurnace : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(140, 90, 40));

            DustType = DustID.Clay;

            HitSound = SoundID.Dig;

            MinPick = 44;

        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<BronzeBrickItem>(), 1);
        }
        public override bool RightClick(int i, int j)
        {
            if (!CheckMultiblock(i, j, out var e))
            {
                Main.NewText("Bronze furnace error! " + e);
                if (!ModTileEntity.ByPosition.TryGetValue(new Point16(i, j), out TileEntity entity))
                {
                    ModTileEntity.PlaceEntityNet(i, j, ModContent.TileEntityType<BronzeFurnaceEntity>());

                }
                return true;
            }
            Main.NewText("Bronze furnace setuped");
            return true;
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            Main.NewText("Place entity");
            ModTileEntity.PlaceEntityNet(i, j, ModContent.TileEntityType<BronzeFurnaceEntity>());
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (ModTileEntity.ByPosition.TryGetValue(new Point16(i, j), out TileEntity entity))
            {
                if (TileEntity.ByPosition.ContainsKey(entity.Position))
                {
                    TileEntity.ByPosition.Remove(entity.Position);
                }
            };


        }

        #region Multiblock
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
            => Main.chest[Chest.FindChest(i + 2, j - 3)];

        #endregion
    }
}