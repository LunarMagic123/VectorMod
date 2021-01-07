using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Tiles.CultTiles
{
	public class CultPackTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			AddMapEntry(new Color(75, 15, 30));

			dustType = 84;
			drop = ItemType<Items.CultSet.CultPack>();
			soundType = SoundID.Tink;
			soundStyle = 1;
			mineResist = 4.5f;
			minPick = 225;
		}

        /*public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
			Tile tile = Framing.GetTileSafely(i, j - 16);
			if (tile.active() && Main.tileSolid[tile.type]) {
				spriteBatch.Draw();
			}
		}*/
    }
}