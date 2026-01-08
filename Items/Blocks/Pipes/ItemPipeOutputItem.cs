using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace Industry.Items.Blocks.Pipes
{
    public class ItemPipeOutputItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.consumable = true;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<Tiles.Pipes.ItemPipeOutputTile>();
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Items.Materials.BronzeBar>(), 8).AddIngredient(ItemID.BrickLayer).Register();
        }
    }
}