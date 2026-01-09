using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Industry.Items.Blocks
{
    public class BronzeItemHatchItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 50);
            Item.createTile = ModContent.TileType<Tiles.Blocks.BronzeItemHatch>();
        }
    }
}
