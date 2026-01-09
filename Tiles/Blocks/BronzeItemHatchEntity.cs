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

namespace Industry.Tiles.Blocks
{
    public class BronzeItemHatchEntity : ModTileEntity
    { 
        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<BronzeItemHatch>();
        }
    }
}