using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Projectiles.BuffedLunarItems
{
	public class SolarStormHandle : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.height = 22;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.localNPCHitCooldown = 1;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 30;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.width = 34;
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
			initialMouse += player.velocity;
			initialPlayer += player.velocity;
			projectile.Center += player.velocity;

			float fixer = (initialMouse - initialPlayer).ToRotation();
			if (fixer * initialVelocity.ToRotation() < 0 && initialVelocity.X < 0)
			{
				fixer = -fixer;
			}

			Notch = (initialVelocity.ToRotation() - fixer) / 15;
			Timer++;
			projectile.Center = player.Center + Vector2.Normalize(initialVelocity).RotatedBy(-Notch * Timer) * Range;

			projectile.velocity -= initialVelocity / 15;
			if (projectile.timeLeft >= 16)
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