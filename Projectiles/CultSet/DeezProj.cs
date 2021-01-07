using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace VectorMod.Projectiles.CultSet
{
	public class DeezProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 56;
			projectile.height = 40;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.timeLeft = 450;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.alpha = 250;
			projectile.usesLocalNPCImmunity = true;
			projectile.extraUpdates = 1;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		int Timer = 0;
		Vector2 targetPos;
		public override void AI() {
			Lighting.AddLight(projectile.Center, 0.8f, 0.2f, 0.0f);
			if (projectile.alpha > 0)
			{
				projectile.alpha -= 50;
			}

			bool hasTarget = false;
			Timer++;
			if (Timer < 120)
			{
				projectile.rotation = projectile.DirectionTo(Main.MouseWorld).ToRotation() + projectile.velocity.Length() * 4 + MathHelper.ToRadians(45);
				projectile.velocity *= 0.98f;
			}
			else if (Timer == 120)
			{
				projectile.velocity = projectile.DirectionTo(Main.MouseWorld) * 15;
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
			}
			else
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npcScan = Main.npc[i];
					if (npcScan.CanBeChasedBy() && Collision.CanHitLine(projectile.Center, 0, 0, npcScan.Center, 0, 0) && (projectile.Center - npcScan.Center).Length() <= 666)
					{
						hasTarget = true;
						targetPos = npcScan.Center;
					}
				}
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
			}
			if (hasTarget)
            {
				projectile.velocity += Vector2.Normalize(targetPos - projectile.Center) * 10;
            }

			if (projectile.velocity.Length() >= 20)
            {
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 20;
            }
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Buffs.HazedD>(), 150);
        }
    }
}
