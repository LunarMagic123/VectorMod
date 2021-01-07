using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VectorMod.Dusts;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class SolarStormBoom : ModProjectile
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
			projectile.melee = true;
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
				Main.PlaySound(SoundID.Item14);
				for (int i = 0; i < 25; i++)
				{
					int dust = Dust.NewDust(projectile.Center, 10, 10, ModContent.DustType<SolarStormDust>(), -20 + Main.rand.Next(40), -20 + Main.rand.Next(40), 100, default, 2f);
					Main.dust[dust].noGravity = true;
				}
			}
			Lighting.AddLight(projectile.Center, 1, 0.3f, 0.4f);

			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 7)
				{
					projectile.frame = 0;
				}
			}
		}
	}
}