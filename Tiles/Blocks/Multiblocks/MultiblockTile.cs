using Terraria.ModLoader;

namespace Industry.Tiles.Blocks.Multiblocks
{
    public abstract class MultiblockTile : ModTile
    {
        protected abstract MultiblockStructure Structure { get; }

        protected bool CheckMultiblock(int i, int j, out string error)
            => Structure.Check(i, j, out error);
    }
}
