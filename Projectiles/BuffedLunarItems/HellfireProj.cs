using VectorMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VectorMod.Buffs;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class HellfireProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.extraUpdates = 1;
			projectile.friendly = true;
			projectile.height = 96;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.localNPCHitCooldown = -1;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 600;
			projectile.usesLocalNPCImmunity = true;
			projectile.width = 38;
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
			return true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			if (projectile.ai[0] == 1f)
			{
				int npcIndex = (int)projectile.ai[1];
				if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
				{
					if (Main.npc[npcIndex].behindTiles)
					{
						drawCacheProjsBehindNPCsAndTiles.Add(index);
					}
					else
					{
						drawCacheProjsBehindNPCs.Add(index);
					}

					return;
				}
			}
			drawCacheProjsBehindProjectiles.Add(index);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = height = 10;
			return true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return projHitbox.Intersects(targetHitbox);
		}

		public bool IsStickingToTarget
		{
			get => projectile.ai[0] == 1f;
			set => projectile.ai[0] = value ? 1f : 0f;
		}

		public int TargetWhoAmI
		{
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		private const int StickCapacity = 8;
		private readonly Point[] _stickingJavelins = new Point[StickCapacity];

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			IsStickingToTarget = true;
			TargetWhoAmI = target.whoAmI;
			projectile.velocity = (target.Center - projectile.Center) * 0.75f;
			projectile.netUpdate = true;
			target.AddBuff(ModContent.BuffType<HellfireD>(), 450);

			projectile.damage = 0;

			KeepSticksBelowMax(target);
		}

		private void KeepSticksBelowMax(NPC target)
		{
			int StickCount = 0;

			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile currentProjectile = Main.projectile[i];
				if (i != projectile.whoAmI && currentProjectile.active && currentProjectile.owner == Main.myPlayer && currentProjectile.type == projectile.type && currentProjectile.modProjectile is HellfireProj Joe && Joe.IsStickingToTarget && Joe.TargetWhoAmI == target.whoAmI)
				{
					_stickingJavelins[StickCount++] = new Point(i, currentProjectile.timeLeft);
					if (StickCount >= _stickingJavelins.Length)
						break;
				}
			}

			if (StickCount >= StickCapacity)
			{
				int OldStickCount = 0;
				for (int i = 1; i < StickCapacity; i++)
				{
					if (_stickingJavelins[i].Y < _stickingJavelins[OldStickCount].Y)
					{
						OldStickCount = i;
					}
				}
				Main.projectile[_stickingJavelins[OldStickCount].X].Kill();
			}
		}

		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 1, 0.3f, 0.4f);

			if (Main.rand.NextBool())
			{
				Dust dust = Main.dust[Dust.NewDust(projectile.Center - (projectile.rotation + MathHelper.ToRadians(90)).ToRotationVector2() * 40 + new Vector2(-4, -4), 8, 8, ModContent.DustType<SolarStormDust>(), projectile.velocity.X / 3, projectile.velocity.Y / 3, 100, default, 2f)];
				dust.noGravity = true;
			}

			if (IsStickingToTarget) StickyAI();
			else NormalAI();
		}

		Vector2 targetPos;
		private void NormalAI()
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
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
				projectile.velocity += Vector2.Normalize(targetPos - projectile.Center);
            }
			else
            {
				projectile.velocity *= 0.97f;
				projectile.rotation = projectile.velocity.ToRotation() + projectile.velocity.Length() + MathHelper.ToRadians(90);
            }

			if (projectile.velocity.Length() >= 20)
            {
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 20;
            }
		}

		private void StickyAI()
		{
			projectile.localAI[0] += 1f;

			if (projectile.localAI[0] >= 600 || TargetWhoAmI < 0 || TargetWhoAmI >= 200)
			{
				projectile.Kill();
			}
			else if (Main.npc[TargetWhoAmI].active && !Main.npc[TargetWhoAmI].dontTakeDamage)
			{
				projectile.Center = Main.npc[TargetWhoAmI].Center - projectile.velocity * 2f;
				projectile.gfxOffY = Main.npc[TargetWhoAmI].gfxOffY;
				if (projectile.localAI[0] % 30f == 0f)
				{
					Main.npc[TargetWhoAmI].HitEffect(0, 1.0);
				}
			}
			else
			{
				projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<SolarStormBoom>(), projectile.damage, 3, Main.player[projectile.owner].whoAmI);
		}
	}
}