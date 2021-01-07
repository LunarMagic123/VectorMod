using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using VectorMod.Buffs;
using VectorMod.Dusts;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class BlazingGazeProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.height = 38;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 900;
			projectile.width = 42;
		}

		int Timer = 0;
		public override void AI()
		{
			projectile.velocity /= 1.02f;

			Vector2 targetPos = new Vector2(0, 0);
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy() && Collision.CanHitLine(projectile.Center, 0, 0, npc.Center, 0, 0) && (npc.Center - projectile.Center).Length() < 500)
				{
					target = true;
					targetPos = npc.Center;
				}
			}
			if (target)
			{
				Timer++;
				if (Timer > 8)
				{
					Projectile.NewProjectile(projectile.Center, Vector2.Normalize(targetPos - projectile.Center).RotatedByRandom(MathHelper.ToRadians(90)) * 10, ProjectileType<Hoba>(), projectile.damage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
					Timer = 0;
				}
			}
			if ((Main.player[projectile.owner].Center - projectile.Center).Length() < 150)
            {
				Main.player[projectile.owner].AddBuff(BuffType<EldritchD>(), 2);
            }
			if (Main.rand.NextBool())
			{
				Dust dust1 = Main.dust[Dust.NewDust(projectile.Center - new Vector2(150, 150), 300, 300, DustType<NebDust1>(), 0, 0, 100, default, 2f)];
				Dust dust2 = Main.dust[Dust.NewDust(projectile.Center - new Vector2(150, 150), 300, 300, DustType<NebDust2>(), 0, 0, 100, default, 2f)];
				dust1.noGravity = true;
				dust2.noGravity = true;
				dust1.velocity = projectile.velocity * 2 + (projectile.Center - dust1.position) / 20;
				dust2.velocity = projectile.velocity * 2 + (projectile.Center - dust2.position) / 20;
			}

			Lighting.AddLight(projectile.Center, 1, 0.5f, 1);

			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 6)
				{
					projectile.frame = 0;
				}
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.player[projectile.owner].AddBuff(BuffType<EldritchB>(), 300);
		}
	}
}