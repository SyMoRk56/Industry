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
                    [ItemID.PlatinumOre] = 4,
                },
                OutputItem = ItemID.PlatinumBar,
                OutputStack = 1,
                CraftTime = 300
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.GoldOre] = 4,
                },
                OutputItem = ItemID.GoldBar,
                OutputStack = 1,
                CraftTime = 300
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.DemoniteOre] = 3,
                },
                OutputItem = ItemID.DemoniteBar,
                OutputStack = 1,
                CraftTime = 360
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.CrimtaneOre] = 3,
                },
                OutputItem = ItemID.CrimtaneBar,
                OutputStack = 1,
                CraftTime = 360
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.Meteorite] = 5,
                },
                OutputItem = ItemID.MeteoriteBar,
                OutputStack = 1,
                CraftTime = 400
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.CopperOre] = 5,
                },
                OutputItem = ItemID.CopperBar,
                OutputStack = 2,
                CraftTime = 120
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.TinOre] = 5,
                },
                OutputItem = ItemID.TinBar,
                OutputStack = 2,
                CraftTime = 110
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.LeadOre] = 5,
                },
                OutputItem = ItemID.LeadBar,
                OutputStack = 2,
                CraftTime = 150
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.IronOre] = 5,
                },
                OutputItem = ItemID.IronBar,
                OutputStack = 2,
                CraftTime = 160
            },
                };
        }
    
        public static class MeteoriteFurnaceRecipes
        {
            public static readonly List<BasicItemToItemRecipe> Recipes =
            new()
                {
                new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.Meteorite] = 3
                },
                OutputItem = ItemID.MeteoriteBar,
                OutputStack = 2,
                CraftTime = 180
            },
                //


                //
                new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.CopperBar] = 3,
                    [ItemID.TinBar] = 1
                },
                OutputItem = ModContent.ItemType<BronzeBar>(),
                OutputStack = 2,
                CraftTime = 120
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.PlatinumOre] = 4,
                },
                OutputItem = ItemID.PlatinumBar,
                OutputStack = 1,
                CraftTime = 200
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.GoldOre] = 4,
                },
                OutputItem = ItemID.GoldBar,
                OutputStack = 1,
                CraftTime = 200
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.DemoniteOre] = 3,
                },
                OutputItem = ItemID.DemoniteBar,
                OutputStack = 1,
                CraftTime = 210
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.CrimtaneOre] = 3,
                },
                OutputItem = ItemID.CrimtaneBar,
                OutputStack = 1,
                CraftTime = 210
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.CopperOre] = 5,
                },
                OutputItem = ItemID.CopperBar,
                OutputStack = 2,
                CraftTime = 80
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.TinOre] = 5,
                },
                OutputItem = ItemID.TinBar,
                OutputStack = 2,
                CraftTime = 70
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.LeadOre] = 5,
                },
                OutputItem = ItemID.LeadBar,
                OutputStack = 2,
                CraftTime = 110
            },
            new BasicItemToItemRecipe
            {
                Inputs =
                {
                    [ItemID.IronOre] = 5,
                },
                OutputItem = ItemID.IronBar,
                OutputStack = 2,
                CraftTime = 120
            },
            };
        }
    }

}
