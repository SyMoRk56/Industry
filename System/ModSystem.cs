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
            if(recipe.createItem.type == ItemID.DemoniteBar || recipe.createItem.type == ItemID.CrimtaneBar)
            {
                recipe.DisableRecipe();
            }
            if (recipe.createItem.type == ItemID.MeteoriteBar)
            {
                recipe.DisableRecipe();
            }
        }
    }
}
