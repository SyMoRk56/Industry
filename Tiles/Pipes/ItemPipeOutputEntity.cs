using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Industry.Tiles.Pipes
{
    public class ItemPipeOutputEntity : ItemPipeEntity
    {
        public override void TryTransfer()
        {
            // output сам ничего не делает
        }

        public bool TryInsert(Item item)
        {
            Main.NewText("Output");
            if (!TryGetChest(new Vector2(Position.X * 16, Position.Y * 16), out Chest chest))
                return false;
            Main.NewText("Output1");

            var output = chest;
            for (int i = 0; i < 40; i++)
            {
                if (output.item[i].IsAir)
                {
                    output.item[i] = item.Clone();
                    output.item[i].stack = 1;
                    return true;
                }

                if (output.item[i].type == item.type &&
                    output.item[i].stack < output.item[i].maxStack)
                {
                    output.item[i].stack++;
                    return true;
                }
            }

            return false;
        }

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<ItemPipeOutputTile>();
        }

    }
}
