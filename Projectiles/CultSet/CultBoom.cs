using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.CultSet
{
	public class CultBoom : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 15;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.friendly = false;
			projectile.height = 388;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.light = 0.5f;
			projectile.localNPCHitCooldown = 2;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 60;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.width = 364;
		}

		public override void AI()
		{
			if (projectile.timeLeft == 60)
			{
				projectile.rotation = MathHelper.ToRadians(Main.rand.Next(180));
			}
			Lighting.AddLight(projectile.Center, 1, 0.3f, 0.4f);

			if (++projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 15)
				{
					projectile.Kill();
				}
			}
			if (projectile.timeLeft <= 21)
            {
				projectile.friendly = true;
			}
			if (projectile.timeLeft == 21)
            {
				for (int i = 0; i < 100; i++)
				{
					Dust dust = Main.dust[Dust.NewDust(projectile.Center, 10, 10, ModContent.DustType<Dusts.HazeDust>(), Main.rand.Next(-25, 25), Main.rand.Next(-25, 25), 0, default, 1.5f)];
					dust.noGravity = true;
					dust.fadeIn = 1f;
				}
			}
		}
	}
}