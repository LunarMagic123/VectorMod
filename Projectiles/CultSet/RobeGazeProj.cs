using VectorMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VectorMod.Buffs;

namespace VectorMod.Projectiles.CultSet
{
	public class RobeGazeProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.extraUpdates = 9;
			projectile.friendly = true;
			projectile.height = 14;
			projectile.hide = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 300;
			projectile.width = 14;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			bool target = false;
			Vector2 targetPos = new Vector2(0, 0);
			for (int k = 0; k < 200; k++)
			{
				NPC npcScan = Main.npc[k];
				if (npcScan.CanBeChasedBy() && (npcScan.Center - projectile.Center).Length() < 1000)
				{
					target = true;
					if ((npcScan.Center - player.Center).Length() <= (targetPos - player.Center).Length())
					{
						targetPos = npcScan.Center;
					}
				}
			}
			if (target)
			{
				projectile.velocity += Vector2.Normalize(targetPos - projectile.Center) * 3;
			}
			
			Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<HazeDust>(), projectile.velocity, 100, default, projectile.timeLeft * 1.5f / 300f);
			dust.noGravity = true;

			if (projectile.velocity.Length() > 10)
            {
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 10;
            }
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Buffs.HazedD>(), 60);
		}
	}
}