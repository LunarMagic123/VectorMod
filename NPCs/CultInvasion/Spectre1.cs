using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.NPCs.CultInvasion
{
	public class Spectre1 : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Occultic Spectre");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults() {
			npc.width = 50;
			npc.height = 64;
			npc.aiStyle = -1;
			npc.damage = 100;
			npc.lifeMax = 12500;
			npc.HitSound = SoundID.NPCHit36;
			npc.DeathSound = SoundID.NPCDeath39;
			npc.value = 2500f;
			npc.noTileCollide = true;
			npc.knockBackResist = 0.25f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.takenDamageMultiplier = 5;
		}

		bool SCATTER = true;
		int projTimer = 0;
		public override void AI()
		{
			if (SCATTER)
			{
				npc.velocity = new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10));
				SCATTER = false;
			}
			Player player = Main.player[npc.target];
			npc.TargetClosest();
			if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && Collision.CanHitLine(npc.Center, 0, 0, player.Center, 0, 0))
			{
				projTimer++;
			}
			int boltCount = 0;
			for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC npcScan = Main.npc[i];
				if (npcScan.type == NPCType<VirulentBolt>())
                {
					boltCount++;
                }
            }
			bool tooManyBolts = false;
			if (boltCount >= 3)
            {
				tooManyBolts = true;
            }
			if (projTimer >= 180 && !tooManyBolts)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<VirulentBolt>(), 0, 0, 0, 0);
				projTimer = 0;
			}

			npc.velocity += Vector2.Normalize(player.Center - npc.Center) / 5;
			if (npc.velocity.Length() >= 10)
            {
				npc.velocity = Vector2.Normalize(npc.velocity) * 10;
			}

			npc.rotation = npc.DirectionTo(player.Center).ToRotation();

			Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustType<Dusts.HazeDust>(), 0f, 0f, 0, default, 1.5f)];
			dust.fadeIn = 1.5f;
			dust.noGravity = true;
			dust.velocity = npc.velocity / 2;
		}

		int frameStay = 0;
		int frameTimer = 0;
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			if (++frameStay >= 3)
            {
				frameStay = 0;
				frameTimer++;
            }
			if (frameTimer >= 4)
            {
				frameTimer = 0;
            }
			npc.frame.Y = frameTimer * frameHeight;
		}

		public override void NPCLoot()
		{
			for (int i = 0; i < Main.rand.Next(1, 3); i++)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Spectre2>(), 0, 0, 0, 0);
			}
		}
	}
}
