using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class Blaz2 : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.height = 140;
			projectile.tileCollide = false;
			projectile.timeLeft = 900;
			projectile.width = 140;
		}

		int Timer = 0;
		public override void AI()
		{
			Timer++;
			if (Timer >= 10)
			{
				projectile.scale *= 1.002f;
				Timer = 0;
			}
			projectile.rotation += MathHelper.ToRadians(8);

			projectile.velocity /= 1.02f;
		}
	}
}