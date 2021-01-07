using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.CultSet
{
	public class HadGazeProj : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 0;
			projectile.friendly = false;
			projectile.hostile = false;
			projectile.timeLeft = 2;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.hide = true;
		}

		public override bool? CanCutTiles()
		{
			return false;
		}

		Vector2 targetPos;
		int gazeTimer = 0;
		public override void AI()
		{
			gazeTimer++;
			Lighting.AddLight(projectile.Center, 0.8f, 0.5f, 0.4f);

			Player player = Main.player[projectile.owner];
			projectile.direction = player.direction;
			projectile.Center = player.Center + new Vector2(0, -16);

			if (VectorModPlayer.CanGaze)
			{
				projectile.timeLeft = 2;
			}
			else
			{
				projectile.Kill();
			}

			bool target = false;
			for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC npcScan = Main.npc[i];
				if (npcScan.CanBeChasedBy() && Collision.CanHitLine(projectile.Center, 0, 0, npcScan.Center, 0, 0) && (npcScan.Center - projectile.Center).Length() < 666)
                {
					target = true;
					if ((npcScan.Center - player.Center).Length() <= (targetPos - player.Center).Length())
                    {
						targetPos = npcScan.Center;
                    }
				}
            }

			if (target && gazeTimer >= (int)Math.Sqrt(player.statLife) * 1.5f && !player.dead)
            {
				Projectile.NewProjectile(projectile.Center, Vector2.Normalize(targetPos - projectile.Center).RotatedByRandom(MathHelper.ToRadians(90)) * 10, ModContent.ProjectileType<RobeGazeProj>(), (int)Math.Sqrt(player.statLifeMax2 - player.statLife) * 5 + 1, 2.5f, player.whoAmI);
				gazeTimer = 0;
			}
		}
    }
}
