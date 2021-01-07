using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.CultSet
{
	public class CultBolt : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.DiamondBolt);
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.penetrate = 10;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 3;
		}

		int turnTimer = 0;
		public override void AI() {
			turnTimer++;
			if (turnTimer >= 3)
            {
				projectile.velocity = Vector2.Normalize(Main.MouseWorld - projectile.Center).RotatedByRandom(2) * 30;
				turnTimer = 0;
			}
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Main.dust[Dust.NewDust(projectile.Center, 10, 10, ModContent.DustType<Dusts.HazeDust>(), 0f, 0f, 0, default, 1.5f)];
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.velocity = projectile.velocity / 5;
			}

			Lighting.AddLight(projectile.Center, 0.8f, 0.2f, 0.0f);
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Buffs.HazedD>(), 300);
        }

        public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, projectile.position);

			Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<CultBoom>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
		}
	}
}
