using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;

namespace Industry.Tiles.Pipes
{
    public class ItemPipeEntity : ModTileEntity
    {
        protected int transferTimer;

        protected const int TransferRate = 120;

        public override void Update()
        {
            
            transferTimer++;

            if (transferTimer >= TransferRate)
            {
                Tile tile = Framing.GetTileSafely(Position.X, Position.Y);

                if (!tile.HasTile || (tile.TileType != ModContent.TileType<ItemPipeTile>() && tile.TileType != ModContent.TileType<ItemPipeInputTile>() && tile.TileType != ModContent.TileType<ItemPipeOutputTile>()))
                {
                    Kill(Position.X, Position.Y);
                }
                transferTimer = 0;
                TryTransfer();
            }
        }
        
        public virtual void TryTransfer()
        {

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

        public override bool IsTileValidForEntity(int x, int y)
        {
            return true;
        }
    }
}
