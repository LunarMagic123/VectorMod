using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Projectiles
{
	public class AuricOrb : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.timeLeft = 180;
			projectile.tileCollide = false;
			projectile.penetrate = 3;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}

		Vector2 targetPos;
		public override void AI() {
			Lighting.AddLight(projectile.Center, 0.5f, 0.4f, 0.3f);

			bool hasTarget = false;
			for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC npcScan = Main.npc[i];
				if (npcScan.CanBeChasedBy() && (npcScan.Center - projectile.Center).Length() <= 300)
                {
					hasTarget = true;
					targetPos = npcScan.Center;
                }
            }
			if (hasTarget)
            {
				projectile.velocity += Vector2.Normalize(targetPos - projectile.Center) / 3;
            }
			else
            {
				projectile.velocity /= 1.03f;
            }
			if (projectile.velocity.Length() >= 15)
            {
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 15;
            }
			projectile.rotation = projectile.velocity.Length() / 2;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, projectile.position);

			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 10, 10, 7, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}
	}
}
