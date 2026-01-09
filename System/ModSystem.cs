using Industry.Items;
using Industry.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Industry.Tiles.Blocks.Multiblocks.Content.Recipes;
using static Industry.System.RecipeSystem;

namespace Industry.System
{
    public class RecipeSystem : ModSystem
    {
        public override void AddRecipes()
        {

        }

        public override void PostAddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == ItemID.PlatinumBar || recipe.createItem.type == ItemID.GoldBar)
                {
                    recipe.DisableRecipe();
                }
                if (recipe.createItem.type == ItemID.DemoniteBar || recipe.createItem.type == ItemID.CrimtaneBar)
                {
                    recipe.DisableRecipe();
                }
                if (recipe.createItem.type == ItemID.MeteoriteBar)
                {
                    recipe.DisableRecipe();
                }
            }
        }
        public static class BronzeFurnaceRecipes
        {
            public static readonly List<BasicItemToItemRecipe> Recipes =
            new()
                {
                new BasicItemToItemRecipe
                {
                    Inputs =
                    {
                        [ItemID.CopperBar] = 3,
                        [ItemID.TinBar] = 1
                    },
                    OutputItem = ModContent.ItemType<BronzeBar>(),
                    OutputStack = 2,
                    CraftTime = 180
                },

                new BasicItemToItemRecipe
                {
                    Inputs =
                    {
                        [ItemID.CopperOre] = 5
                    },
                    OutputItem = ItemID.CopperBar,
                    OutputStack = 2,
                    CraftTime = 120
                },
                };
        }
    }

}
