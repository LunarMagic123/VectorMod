using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.FTProjs
{
	public class SinFlame : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.timeLeft = 30;
			projectile.ignoreWater = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
			projectile.penetrate = 4;
			projectile.hide = true;
		}

		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.4f, 0.4f, 0.6f);

			projectile.rotation = projectile.velocity.ToRotation();

			for (int i = 0; i < 3; i++)
			{
				Dust dust = Main.dust[Dust.NewDust(projectile.Center + new Vector2(-24, -24), 48, 48, ModContent.DustType<Dusts.HazeDust>(), 0f, 0f, 0, default, 2)];
				dust.fadeIn = 1.5f;
				dust.noGravity = true;
				dust.velocity = projectile.velocity / 5;
			}

			if (projectile.velocity.Length() != 30)
			{
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 30;
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Buffs.HazedD>(), 300);
        }
    }
}
