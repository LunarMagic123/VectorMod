using VectorMod.Buffs;
using VectorMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Projectiles.CultSet
{
	public class PlateHazerP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 34;
			projectile.height = 42;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 2;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 4;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D Texture = GetTexture("VectorMod/Projectiles/CultSet/PlateHazerP");
			Rectangle arrowRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
			Vector2 dir = projectile.velocity;
			dir.Normalize();
			for (int i = 0; i < 5; i++)
			{
				Vector2 drawPos = projectile.Center;
				drawPos -= dir * new Vector2(Math.Abs(projectile.velocity.X), Math.Abs(projectile.velocity.Y)) * i;

				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - i) / projectile.oldPos.Length);

				spriteBatch.Draw(Texture, drawPos - Main.screenPosition, arrowRect, color, projectile.rotation, new Vector2(Texture.Width, Texture.Height) / 2, 1f, SpriteEffects.None, 0f);
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

		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.8f, 0.2f, 0.0f);

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			Player player = Main.player[projectile.owner];
			if (player.dead || !player.active) {
				player.ClearBuff(BuffType<HazerM>());
			}
			if (player.HasBuff(BuffType<HazerM>())) {
				projectile.timeLeft = 2;
			}

			bool hasTarget = false;
			Vector2 targetPos = new Vector2(player.Center.X, player.Center.Y - 75);
			for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC npcScan = Main.npc[i];
				if (npcScan.CanBeChasedBy() && (projectile.Center - npcScan.Center).Length() <= 750 || npcScan.CanBeChasedBy() && (player.Center - npcScan.Center).Length() <= 1000)
                {
					hasTarget = true;
					targetPos = npcScan.Center;
                }
            }
			if (hasTarget)
            {
				if ((projectile.Center - targetPos).Length() >= 100)
				{
					projectile.velocity += Vector2.Normalize(targetPos - projectile.Center) * 25;
				}
				if (projectile.velocity.Length() >= 50)
                {
					projectile.velocity = Vector2.Normalize(projectile.velocity) * 50;
                }
            }
			else
			{
				if ((projectile.Center - targetPos).Length() >= 75)
				{
					projectile.velocity += Vector2.Normalize(targetPos - projectile.Center);
				}
				if (projectile.velocity.Length() >= (projectile.Center - targetPos).Length() / 10)
				{
					projectile.velocity = Vector2.Normalize(projectile.velocity) * (projectile.Center - targetPos).Length() / 10;
				}
			}
			if ((targetPos - projectile.Center).Length() >= 2000)
            {
				projectile.Center = targetPos;
            }
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, projectile.position);

			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustType<HazeDust>(), projectile.velocity.X, projectile.velocity.Y, 100, default, 1f);
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffType<Buffs.HazedD>(), 60);
		}
	}
}
