using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class BetterBeaterBoom : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.height = 102;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.localNPCHitCooldown = -1;
			projectile.ranged = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 35;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.width = 82;
		}

		public override void AI()
		{
			if (projectile.timeLeft == 35)
            {
				projectile.rotation = MathHelper.ToRadians(Main.rand.Next(360));
			}
			Lighting.AddLight(projectile.Center, 0, 0.8f, 0.6f);

			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 7)
				{
					projectile.Kill();
				}
			}
		}
	}
}