using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace Industry.Tiles.Pipes
{
    public class ItemPipeTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
        }

        public override bool RightClick(int i, int j)
        {
            Vector2 worldPos = new Vector2(i * 16, j * 16);
            if (ModTileEntity.ByPosition.TryGetValue(new Point16(i, j), out TileEntity entity))
            {
                if (TileEntity.ByPosition.ContainsKey(entity.Position))
                {
                    Main.NewText("EEEEntity");
                }
            };

            return true;
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                ModTileEntity.PlaceEntityNet(i, j,
                    ModContent.TileEntityType<ItemPipeEntity>());
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
        public override bool CanPlace(int i, int j)
        {
            return true;
        }

    }
}
