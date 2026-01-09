using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Industry.Tiles.Blocks.Multiblocks;

namespace Industry.Tiles.Blocks.Multiblocks.Content.Structures
{
    public static class BronzeFurnaceStructure
    {
        public static readonly MultiblockStructure Instance =
            new MultiblockStructure(
                new Dictionary<Point, int>
                {



                    [new(-1, 0)] = ModContent.TileType<BronzeBrick>(),
                    [new(1, 0)] = ModContent.TileType<BronzeBrick>(),
                    [new(-2, 0)] = ModContent.TileType<BronzeBrick>(),
                    [new(-2, 0)] = ModContent.TileType<BronzeBrick>(),

                    [new(-2, -1)] = ModContent.TileType<BronzeBrick>(),
                    [new(-1, -1)] = ModContent.TileType<BronzeBrick>(),
                    [new(0, -1)] = ModContent.TileType<BronzeBrick>(),
                    [new(1, -1)] = ModContent.TileType<BronzeBrick>(),
                    [new(2, -1)] = ModContent.TileType<BronzeBrick>(),

                    [new(-1, 1)] = TileID.RedBrick,
                    [new(1, 1)] = TileID.RedBrick,
                    [new(0, 1)] = TileID.RedBrick,
                    [new(-2, 1)] = TileID.RedBrick,
                    [new(-2, 1)] = TileID.RedBrick,

                    [new(-1, -4)] = TileID.RedBrick,
                    [new(1, -4)] = TileID.RedBrick,
                    [new(0, -4)] = TileID.RedBrick,
                    [new(-2, -4)] = TileID.RedBrick,
                    [new(-2, -4)] = TileID.RedBrick,

                    [new(0, -2)] = ModContent.TileType<BronzeBrick>(),
                    [new(0, -3)] = ModContent.TileType<BronzeBrick>(),
                },
                new Dictionary<Point, int>
                {
                    [new(-2, -3)] = ModContent.TileType<BronzeItemHatch>(), // input
                    [new(1, -3)] = ModContent.TileType<BronzeItemHatch>()  // output
                }
            );
    }
}
