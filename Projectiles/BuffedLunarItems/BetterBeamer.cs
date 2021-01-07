using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class BetterBeamer : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 6;
		}

		private const float MOVE_DISTANCE = 60f;

		public float Distance;

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = true;
			projectile.ranged = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
			projectile.timeLeft = 60;
		}

		Vector2 origSpawn;
		int frameTimer = 0;
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], origSpawn,
			projectile.velocity * 6, 10, -1.57f, 1f, default, default, MOVE_DISTANCE / 2);
			return false;
		}

		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), float transDist = 50)
		{
			Color c = Color.White;
			float r = unit.ToRotation() + rotation;

			for (float i = transDist; i <= Distance; i += step)
			{
				var origin = start + (i - 3) * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle(0, 360 + frameTimer * 60, 24, 60), i < transDist ? Color.Transparent : c, r, new Vector2(12, 30), scale, 0, 0);
			}

			spriteBatch.Draw(texture, start + unit * (transDist - step - 3) - Main.screenPosition, new Rectangle(0, 0 + frameTimer * 60, 24, 60), c, r, new Vector2(12, 30), scale, 0, 0);

			spriteBatch.Draw(texture, start + unit * (Distance + step - 3) - Main.screenPosition, new Rectangle(0, 720 + frameTimer * 60, 24, 60), c, r, new Vector2(12, 60), scale, 0, 0);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Vector2 v = projectile.velocity;
			float point = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), origSpawn, origSpawn + v * Distance, 22, ref point);
		}

		int saved = 0;
		int Timer = 0;
		public override void AI()
		{
			Timer++;
			if (Timer >= 10)
            {
				frameTimer++;
				Timer = 0;
            }
			if (frameTimer > 6)
            {
				projectile.Kill();
            }

			if (saved == 0)
            {
				origSpawn = projectile.position;
				saved = 1;
            }
			projectile.position = origSpawn;

			SetLaserPosition();
			CastLights();
		}

		private void SetLaserPosition()
		{
			for (Distance = MOVE_DISTANCE; Distance <= 3600; Distance += 60)
			{
				var start = origSpawn + projectile.velocity * Distance;
				if (!Collision.CanHit(origSpawn, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					break;
				}
			}
		}

		private void CastLights()
		{
			DelegateMethods.v3_1 = new Vector3(0.1f, 0.2f, 2f);
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = projectile.velocity;
			Utils.PlotTileLine(projectile.Center, projectile.Center + unit * Distance, (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
		}
	}
}
