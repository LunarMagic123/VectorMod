using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Dusts
{
	public class NebDust2 : ModDust
	{
		public override void OnSpawn(Dust dust) {
			dust.velocity.Y = Main.rand.Next(-10, 6) * 0.1f;
			dust.velocity.X *= 0.3f;
		}

		public override bool MidUpdate(Dust dust) {
			if (dust.noLight) {
				return false;
			}
			Lighting.AddLight(dust.position, 0.8f, 0.2f, 0.6f);
			return false;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor) 
			=> new Color(lightColor.R, lightColor.G, lightColor.B, 25);
	}
}