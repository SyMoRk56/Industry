using Industry.Tiles.Blocks.Multiblocks.Content;
using Industry.Tiles.Blocks.Multiblocks.Content.Structures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Industry.Tiles.Blocks.Multiblocks.Content.Recipes;
using Industry.Tiles.Blocks.Multiblocks;
using System.Collections.Generic;

namespace Industry.Tiles
{
    public class BronzeFurnaceEntity
        : MultiblockEntity<BasicItemToItemRecipe>
    {
        // ===== ФОРМА МУЛЬТИБЛОКА =====
        protected override MultiblockStructure Structure
            => BronzeFurnaceStructure.Instance;

        // ===== РЕЦЕПТЫ =====
        protected override IEnumerable<BasicItemToItemRecipe> Recipes
            => System.RecipeSystem.BronzeFurnaceRecipes.Recipes;

        // ===== СУНДУКИ =====
        protected override bool TryGetIO(out Chest input, out Chest output)
        {
            int i = Position.X;
            int j = Position.Y;

            input = Structure.GetChest(i, j, new Point(-2, -3));
            output = Structure.GetChest(i, j, new Point(1, -3));

            return input != null && output != null;
        }

        // ===== ПРОВЕРКА ТАЙЛА =====
        public override bool IsTileValidForEntity(int x, int y)
        {
            return Main.tile[x, y].HasTile &&
                   Main.tile[x, y].TileType ==
                   ModContent.TileType<BronzeFurnace>();
        }
    }
}
