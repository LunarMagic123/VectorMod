using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.ID;

namespace VectorMod.Projectiles.CultSet
{
	public class CultYoyoSterile : ModProjectile
	{
		public override void SetStaticDefaults()
        {
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 9;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.alpha = 250;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 3;
			projectile.extraUpdates = 1;
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}

			var player = Main.player[projectile.owner];
			Vector2 mountedCenter = player.MountedCenter;
			Texture2D chainTexture = GetTexture("VectorMod/Projectiles/CultSet/YoyoString");
			var drawPosition = projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;
			float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

			if (projectile.alpha == 0)
			{
				int direction = -1;

				if (projectile.Center.X < mountedCenter.X)
					direction = 1;

				player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
			}

			while (true)
			{
				float length = remainingVectorToPlayer.Length();

				if (length < 10 || float.IsNaN(length))
					break;
				drawPosition += remainingVectorToPlayer * 10 / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;
				spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, null, new Color((int)remainingVectorToPlayer.Length() / 3, 0, (int)Math.Sqrt(remainingVectorToPlayer.Length()) * 3, 0), rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			}

			return true;
		}

		int bitchTimer = 0;
		public override void AI() {
			bitchTimer++;
			Player player = Main.player[projectile.owner];
			Lighting.AddLight(projectile.Center, 0.6f, 0.2f, 0.0f);
			if (projectile.alpha > 0)
			{
				projectile.alpha -= 50;
			}

			if (player.channel)
			{
				projectile.timeLeft = 60;
				if (bitchTimer < 25)
				{
					projectile.velocity /= 1.02f;
				}
				else
				{
					for (int i = 0; i < Main.maxProjectiles; i++)
                    {
						Projectile projScan = Main.projectile[i];
						if (projectile.type == ProjectileType<CultYoyoFertile>() && projScan.owner == projectile.owner)
						{
							Vector2 targetPos = projScan.Center;
							if ((targetPos - projectile.Center).Length() > 50)
							{
								projectile.velocity += Vector2.Normalize(targetPos + projectile.velocity - projectile.Center);
							}
						}
                    }
				}
			}
			else
            {
				projectile.velocity += Vector2.Normalize(player.Center - projectile.Center) * 5 * player.meleeSpeed;
            }
			if (!player.channel && (player.Center - projectile.Center).Length() <= 20)
            {
				projectile.Kill();
            }

			if (projectile.velocity.Length() >= 15 * player.meleeSpeed)
            {
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 15 * player.meleeSpeed;
            }
			projectile.rotation += 0.15f * player.meleeSpeed;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player player = Main.player[projectile.owner];
			if (player.channel)
			{
				bitchTimer = 0;
				projectile.position -= projectile.velocity * 3f;
				projectile.velocity *= -0.5f;
			}
		}
    }
}
