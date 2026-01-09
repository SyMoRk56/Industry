using Terraria;
using Industry.Tiles.Blocks.Multiblocks.Content.Structures;
using Industry.Tiles.Blocks.Multiblocks;

namespace Industry.Tiles.Blocks.Multiblocks.Content
{
    public class BronzeFurnace : MultiblockTile
    {
        protected override MultiblockStructure Structure
            => BronzeFurnaceStructure.Instance;

        public override bool RightClick(int i, int j)
        {
            if (!CheckMultiblock(i, j, out string error))
            {
                Main.NewText(error);
                return true;
            }

            Main.NewText("Bronze Furnace ready");
            return true;
        }
    }
}
