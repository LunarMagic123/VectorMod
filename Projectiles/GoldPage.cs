using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Projectiles
{
	public class GoldPage : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 26;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.timeLeft = 180;
			projectile.penetrate = 2;
		}

		public override void AI()
        {
			projectile.rotation += 0.5f;
        }

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(projectile.Center, 5, 5, 210, 0, 0, 100, default, 1f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}
	}
}
