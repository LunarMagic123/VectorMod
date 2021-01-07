using VectorMod.Buffs;
using VectorMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class StarSlicerBlue : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.extraUpdates = 2;
			projectile.friendly = true;
			projectile.height = 10;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.localNPCHitCooldown = 18;
			projectile.minion = true;
			projectile.minionSlots = 0.5f;
			projectile.netImportant = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 2;
			projectile.usesLocalNPCImmunity = true;
			projectile.width = 30;
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

		public override bool? CanCutTiles()
		{
			return false;
		}

		public override bool MinionContactDamage()
		{
			return true;
		}

		Vector2 targetPos;
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.3f, 0.2f, 0.1f);

			Player player = Main.player[projectile.owner];
			if (player.dead || !player.active)
			{
				player.ClearBuff(BuffType<StarSlicerM>());
			}
			if (player.HasBuff(BuffType<StarSlicerM>()))
			{
				projectile.timeLeft = 2;
			}

			bool hasTarget = false;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npcScan = Main.npc[i];
				if (npcScan.CanBeChasedBy() && Collision.CanHitLine(projectile.Center, 0, 0, npcScan.Center, 0, 0) && (npcScan.Center - projectile.Center).Length() < 800)
				{
					hasTarget = true;
					targetPos = npcScan.Center;
				}
			}
			if (hasTarget)
			{
				if ((targetPos - projectile.Center).Length() > 50)
				{
					projectile.velocity += Vector2.Normalize(targetPos - projectile.Center) * 2;
				}
				if (projectile.velocity.Length() >= 15)
				{
					projectile.velocity = Vector2.Normalize(projectile.velocity) * 15;
				}
				for (int j = 1; j < 3; j++)
				{
					Dust dust = Dust.NewDustPerfect(projectile.Center, DustType<StardustBlue>(), Vector2.Normalize(projectile.velocity) * j * 5, 100, default, 1.5f);
					dust.noGravity = true;
				}
			}
			else
			{
				targetPos = player.Center;
				if ((player.Center - projectile.Center).Length() > 150)
				{
					projectile.velocity += Vector2.Normalize(targetPos - projectile.Center) / 3;
				}
				if (projectile.velocity.Length() > 7.5f)
				{
					projectile.velocity = Vector2.Normalize(projectile.velocity) * 7.5f;
				}
			}

			if ((player.Center - projectile.Center).Length() > 1200)
			{
				projectile.Center = player.Center;
			}

			projectile.rotation = projectile.velocity.ToRotation();
		}
	}
}