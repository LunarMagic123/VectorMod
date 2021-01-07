using VectorMod.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VectorMod.Buffs;
using System;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class Hoba : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.extraUpdates = 4;
			projectile.friendly = true;
			projectile.height = 14;
			projectile.hide = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 300;
			projectile.width = 14;
		}

		public override void AI()
		{
			bool target = false;
			Vector2 targetPos = new Vector2(0, 0);
			for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];
				if (npc.CanBeChasedBy() && Collision.CanHitLine(projectile.Center, 0, 0, npc.Center, 0, 0) && (npc.Center - projectile.Center).Length() < 1000)
				{
					target = true;
					targetPos = npc.Center;
				}
			}
			if (target)
			{
				projectile.velocity += Vector2.Normalize(targetPos - projectile.Center);
			}

			if (Main.rand.NextBool() == true)
			{
				Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<NebDust1>(), -projectile.velocity, 100, default, projectile.timeLeft * 1.5f / 300f);
				dust.noGravity = true;
			}
			else
			{
				Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<NebDust2>(), -projectile.velocity, 100, default, projectile.timeLeft * 1.5f / 300f);
				dust.noGravity = true;

			}

			if (projectile.velocity.Length() > 10)
            {
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 10;
            }
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.player[projectile.owner].AddBuff(ModContent.BuffType<EldritchB>(), 300);
		}
	}
}