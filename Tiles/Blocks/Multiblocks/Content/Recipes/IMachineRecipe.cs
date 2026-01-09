using Terraria;

namespace Industry.Tiles.Blocks.Multiblocks.Content.Recipes
{
    public interface IMachineRecipe
    {
        int CraftTime { get; }

        bool CanCraft(Chest input);
        bool CanOutputFit(Chest output);

        void ConsumeInput(Chest input);
        void ProduceOutput(Chest output);
    }
}
