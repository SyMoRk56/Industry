using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace Industry.Tiles.Pipes
{
    public class ItemPipeInputTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

            // 🔥 КЛЮЧЕВОЕ
            TileObjectData.newTile.AnchorBottom = default;
            TileObjectData.newTile.AnchorTop = default;
            TileObjectData.newTile.AnchorLeft = default;
            TileObjectData.newTile.AnchorRight = default;

            TileObjectData.addTile(Type);

            AddMapEntry(Color.Gray);
        }
        public override bool CanPlace(int i, int j)
        {
            // смещения: влево, вправо, вверх, вниз
            Point[] offsets =
            {
        new Point(-1, 0),
        new Point( 1, 0),
        new Point( 0,-1),
        new Point( 0, 1),
    };

            foreach (Point offset in offsets)
            {
                int x = i + offset.X;
                int y = j + offset.Y;

                Tile tile = Framing.GetTileSafely(x, y);

                if (tile.HasTile && TileID.Sets.IsAContainer[tile.TileType])
                    return true;
            }

            return false;
        }

        public override bool RightClick(int i, int j)
        {
            Vector2 worldPos = new Vector2(i * 16, j * 16);

            bool found = TryGetChest(worldPos, out var chest);
            Main.NewText(found);
            return true;
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                ModTileEntity.PlaceEntityNet(i, j,
                    ModContent.TileEntityType<ItemPipeInputEntity>());
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Main.NewText("Kill");
            if (ModTileEntity.ByPosition.TryGetValue(new Point16(i * 16, j * 16), out TileEntity entity))
            {
                Main.NewText("Kill1");

                if (TileEntity.ByPosition.ContainsKey(entity.Position))
                {
                    Main.NewText("Kill2");

                    TileEntity.ByPosition.Remove(entity.Position);
                }
            };


        }
        protected bool TryGetChest(Vector2 position, out Chest chest)
        {
            chest = null;
            int range = 2;

            int tileX = (int)(position.X / 16f);
            int tileY = (int)(position.Y / 16f);

            int chestIndex = -1;
            float minDistance = float.MaxValue;

            for (int x = tileX - range; x <= tileX + range; x++)
            {
                for (int y = tileY - range; y <= tileY + range; y++)
                {
                    if (!WorldGen.InWorld(x, y))
                        continue;

                    Tile tile = Framing.GetTileSafely(x, y);
                    if (tile == null || !tile.HasTile)
                        continue;

                    // Проверяем, принадлежит ли тайл сундуку
                    if (TileID.Sets.BasicChest[tile.TileType])
                    {
                        // Попробуем найти сундук с координатами этого тайла
                        int index = Chest.FindChest(x - tile.TileFrameX / 36, y - tile.TileFrameY / 36);

                        if (index >= 0)
                        {
                            float dist = Vector2.Distance(position, new Vector2(x * 16f + 8f, y * 16f + 8f));
                            if (dist < minDistance)
                            {
                                minDistance = dist;
                                chestIndex = index;
                            }
                        }
                    }
                }
            }

            if (chestIndex >= 0)
            {
                chest = Main.chest[chestIndex];
                return true;
            }

            return false;
        }
    }
}
