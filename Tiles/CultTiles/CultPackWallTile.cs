using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Tiles.CultTiles
{
	public class CultPackWallTile : ModWall
	{
		public override void SetDefaults() {
			Main.wallHouse[Type] = true;
			drop = ItemType<Items.CultSet.CultPackWall>();
			AddMapEntry(new Color(45, 15, 30));
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
			r = 0.18f;
			g = 0.06f;
			b = 0.12f;
		}
	}
}