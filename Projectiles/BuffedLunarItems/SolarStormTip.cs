using VectorMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class SolarStormTip : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.extraUpdates = 2;
			projectile.friendly = true;
			projectile.height = 54;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.localNPCHitCooldown = 1;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 90;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
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
			Dust dust = Main.dust[Dust.NewDust(projectile.Center, 10, 10, ModContent.DustType<SolarStormDust>(), Vector2.Normalize(projectile.velocity).X * 10, Vector2.Normalize(projectile.velocity).Y * 10, 100, default, 2f)];
			dust.noGravity = true;

			Player player = Main.player[projectile.owner];
			if (saved == false)
            {
				initialMouse = Main.MouseWorld;
				initialPlayer = player.Center;
				initialVelocity = projectile.velocity;
				Main.PlaySound(SoundID.Item116, projectile.position);
				saved = true;
			}
			initialMouse += player.velocity / 3;
			initialPlayer += player.velocity / 3;
			projectile.Center += player.velocity / 3;

			float fixer = (initialMouse - initialPlayer).ToRotation();
			if (fixer * initialVelocity.ToRotation() < 0 && initialVelocity.X < 0)
			{
				fixer = -fixer;
			}

			Notch = (initialVelocity.ToRotation() - fixer) / 45;
			Timer++;
			projectile.Center = player.Center + Vector2.Normalize(initialVelocity).RotatedBy(-Notch * Timer) * Range;

			projectile.velocity -= initialVelocity / 45;
			if (projectile.timeLeft >= 46)
			{
				Range += projectile.velocity.Length() / 1.5f;
			}
			else
			{
				Range -= projectile.velocity.Length() / 1.5f;
			}

			projectile.rotation = projectile.DirectionTo(player.Center).ToRotation() - MathHelper.ToRadians(90);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int chance = Main.rand.Next(3);
			if (chance == 0)
			{
				Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<SolarStormBoom>(), damage, 3, Main.player[projectile.owner].whoAmI);
			}
			target.AddBuff(ModContent.BuffType<Buffs.HellfireD>(), 360);
		}
	}
}