using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Industry.Tiles.Blocks.Multiblocks
{
    public class MultiblockStructure
    {
        public readonly Dictionary<Point, int> RequiredTiles;
        public readonly Dictionary<Point, int> RequiredChests;

        public MultiblockStructure(
            Dictionary<Point, int> tiles,
            Dictionary<Point, int> chests = null)
        {
            RequiredTiles = tiles;
            RequiredChests = chests ?? new();
        }

        public bool Check(int originX, int originY, out string error)
        {
            error = null;

            foreach (var kv in RequiredTiles)
            {
                Point p = kv.Key;
                int type = kv.Value;

                Tile tile = Framing.GetTileSafely(originX + p.X, originY + p.Y);
                if (!tile.HasTile || tile.TileType != type)
                {
                    error = $"Нарушен мультиблок ({p.X:+#;-#;0},{p.Y:+#;-#;0})";
                    return false;
                }
            }

            foreach (var kv in RequiredChests)
            {
                Point p = kv.Key;

                int index = Chest.FindChest(originX + p.X, originY + p.Y);
                if (index < 0)
                {
                    error = $"Нет сундука ({p.X:+#;-#;0},{p.Y:+#;-#;0})";
                    return false;
                }

                if (Main.tile[originX + p.X, originY + p.Y].TileType != TileID.Containers)
                {
                    error = "Неверный тип сундука";
                    return false;
                }
            }

            return true;
        }

        public Chest GetChest(int originX, int originY, Point offset)
        {
            int index = Chest.FindChest(originX + offset.X, originY + offset.Y);
            return index >= 0 ? Main.chest[index] : null;
        }
    }
}
