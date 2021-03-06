using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Dusts
{
	public class StardustBlue : ModDust
	{
		public override bool MidUpdate(Dust dust)
		{
			if (dust.noLight)
			{
				return false;
			}

			/*Lighting.AddLight(dust.position, 0.1f, 0.2f, 0.3f);*/

			return false;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor)
			=> new Color(0.3f, 0.25f, 0.3f, 0);
	}
}