using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class SolarStormChainSmall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.extraUpdates = 1;
			projectile.friendly = true;
			projectile.height = 26;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
			projectile.width = 34;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		bool saved = false;
		float Notch;
		float Range = 0;
		int Timer = 0;
		Vector2 initialMouse;
		Vector2 initialPlayer;
		Vector2 initialVelocity;
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (saved == false)
			{
				initialMouse = Main.MouseWorld;
				initialPlayer = player.Center;
				initialVelocity = projectile.velocity;
				saved = true;
			}
			initialMouse += player.velocity / 2;
			initialPlayer += player.velocity / 2;
			projectile.Center += player.velocity / 2;

			float fixer = (initialMouse - initialPlayer).ToRotation();
			if (fixer * initialVelocity.ToRotation() < 0 && initialVelocity.X < 0)
			{
				fixer = -fixer;
			}

			Notch = (initialVelocity.ToRotation() - fixer) / 30;
			Timer++;
			projectile.Center = player.Center + Vector2.Normalize(initialVelocity).RotatedBy(-Notch * Timer) * Range;

			projectile.velocity -= initialVelocity / 30;
			if (projectile.timeLeft >= 31)
			{
				Range += projectile.velocity.Length();
			}
			else
			{
				Range -= projectile.velocity.Length();
			}

			projectile.rotation = projectile.DirectionTo(player.Center).ToRotation() - MathHelper.ToRadians(90);
		}
	}
}