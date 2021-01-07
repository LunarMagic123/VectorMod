using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class BetterBeaterRock : ModProjectile
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
			projectile.height = 28;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.tileCollide = true;
			projectile.timeLeft = 120;
			projectile.width = 18;
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

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + (MathHelper.ToRadians(90));

			Lighting.AddLight(projectile.Center, 0, 0.4f, 0.3f);

			Dust dust = Dust.NewDustPerfect(projectile.Center - (projectile.velocity / 3), 226, Vector2.Zero, 0, default);
			dust.velocity = projectile.velocity / 3;
			dust.noGravity = true;

		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<BetterBeaterBoom>(), projectile.damage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
		}
	}
}