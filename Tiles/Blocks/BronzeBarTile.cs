using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Industry.Tiles
{
    public class BronzeBarTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
            MinPick = 44;
            AddMapEntry(new Color(200, 200, 200));

            HitSound = SoundID.Tink;
            DustType = DustID.Silver;

            
        }
    }
}