using Industry.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Industry.Items
{
    public class BronzeBrickItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<Tiles.BronzeBrick>();
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<BronzeBar>(),8).AddIngredient(ItemID.BrickLayer).Register();
        }
    }
}