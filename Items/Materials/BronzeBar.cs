using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Industry.Items.Materials
{
    public class BronzeBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 64;

            Item.value = 200;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            
            Item.createTile = ModContent.TileType<Tiles.BronzeBarTile>();

        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.CopperBar, 3).AddIngredient(ItemID.TinBar, 1).AddTile(TileID.Furnaces).Register();
        }
    }
}