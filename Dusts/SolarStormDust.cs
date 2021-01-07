using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Dusts
{
	public class SolarStormDust : ModDust
	{
		public override bool MidUpdate(Dust dust)
		{
			if (dust.noLight)
			{
				return false;
			}

			Lighting.AddLight(dust.position, 1, 0.3f, 0.4f);

			return false;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor)
			=> new Color(1, 0.3f, 0.4f, 0);
	}
}