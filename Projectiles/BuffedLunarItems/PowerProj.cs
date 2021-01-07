using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class PowerProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.extraUpdates = 1;
			projectile.friendly = true;
			projectile.height = 22;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 900;
			projectile.width = 22;
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
			Lighting.AddLight(projectile.Center, 0.4f, 0.2f, 0.4f);

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
				Player player = Main.player[projectile.owner];
				targetPos = player.Center;
				if ((player.Center - projectile.Center).Length() > 75)
				{
					projectile.velocity += Vector2.Normalize(targetPos + projectile.velocity - projectile.Center) * 1.5f;
				}
			}

			if (projectile.velocity.Length() > 12.5f)
			{
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 12.5f;
			}
		}
    }
}