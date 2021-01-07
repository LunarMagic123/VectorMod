using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class WalkerArrowProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.extraUpdates = 1;
			projectile.friendly = true;
			projectile.height = 16;
			projectile.hostile = false;
			projectile.light = 0.5f;
			projectile.ranged = true;
			projectile.tileCollide = true;
			projectile.timeLeft = 600;
			projectile.width = 16;
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

		Vector2 targetPos;
		public override void AI()
		{
			bool hasTarget = false;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npcScan = Main.npc[i];
				if (npcScan.CanBeChasedBy() && Collision.CanHitLine(projectile.Center, 0, 0, npcScan.Center, 0, 0) && (projectile.Center - npcScan.Center).Length() <= 666)
				{
					hasTarget = true;
					targetPos = npcScan.Center;
				}
			}
			if (hasTarget)
			{
				projectile.velocity += Vector2.Normalize(targetPos - projectile.Center);
			}
			else
            {
				if (projectile.velocity.X > 0)
				{
					projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(0.25f));
				}
				else
				{
					projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-0.25f));
				}
            }
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(-90);
			Lighting.AddLight(projectile.Center, 0, 0.4f, 0.3f);

			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustPerfect(projectile.Center, 226, Vector2.Zero, 0, default);
				dust.velocity = projectile.velocity;
				dust.noGravity = true;
			}

			if (projectile.velocity.Length() >= 15)
            {
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 15;
            }
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[projectile.owner];
			Projectile.NewProjectile(player.Center, Vector2.Normalize(target.Center - player.Center) * projectile.velocity.Length(), ModContent.ProjectileType<WalkerSpawn>(), projectile.damage, projectile.knockBack, player.whoAmI);
			target.AddBuff(BuffID.Electrified, 180);
		}
	}
}