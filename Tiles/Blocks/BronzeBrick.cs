using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Industry.Items;
using System.Collections.Generic;
using Industry.Items.Materials;
namespace Industry.Tiles
{
    public class BronzeBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type]= true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(new Color(150, 100, 50));

            DustType = DustID.Clay;

            HitSound = SoundID.Dig;

            MinPick = 44;
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ModContent.ItemType<BronzeBar>(),Main.rand.Next(3, 6));
        }
    }
}
