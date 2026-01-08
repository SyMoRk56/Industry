using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Industry.Tiles.Pipes;
using System.Net;
using Terraria.DataStructures;

namespace Industry.Tiles.Pipes
{
    public class ItemPipeInputEntity : ItemPipeEntity
    {
        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<ItemPipeInputTile>();
        }
        public override void TryTransfer()
        {
            Main.NewText("TryTransfer " + Position);
            Tile tile = Framing.GetTileSafely(Position.X, Position.Y);
            Main.NewText("ooo");
            Main.NewText((!tile.HasTile || (tile.TileType != ModContent.TileType<ItemPipeTile>() && tile.TileType != ModContent.TileType<ItemPipeInputTile>() && tile.TileType != ModContent.TileType<ItemPipeOutputTile>())));
            if (!tile.HasTile || (tile.TileType != ModContent.TileType<ItemPipeTile>() && tile.TileType != ModContent.TileType<ItemPipeInputTile>() && tile.TileType != ModContent.TileType<ItemPipeOutputTile>()))
            {
                if(TileEntity.ByPosition.TryGetValue(Position, out var value))
                {
                    TileEntity.ByPosition.Remove(Position);
                }

                
            }
            if (TryGetChest(new Vector2(Position.X * 16, Position.Y * 16), out Chest chest))
            {

                var input = chest;
                if (!PipeNetwork.TryFindOutput(Position, out ItemPipeOutputEntity output))
                    return;

                Item item = FindItem(input);
                if (item == null)
                    return;

                if (!output.TryInsert(item))
                    return;

                item.stack--;
                if (item.stack <= 0)
                    item.TurnToAir();
            }
            else
            {
                Main.NewText("Do not get chest");
                return;
            }
        }

        private Item FindItem(Chest chest)
        {
            foreach (var item in chest.item)
                if (!item.IsAir)
                    return item;

            return null;
        }
    }
}
