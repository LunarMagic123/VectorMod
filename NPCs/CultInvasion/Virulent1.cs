using VectorMod.Items.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;

namespace VectorMod.NPCs.CultInvasion
{
	public class Virulent1 : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Virulent Beast");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults() {
			npc.width = 172;
			npc.height = 140;
			npc.aiStyle = -1;
			npc.damage = 90;
			npc.defense = 50;
			npc.lifeMax = 45000;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath5;
			npc.value = 333333f;
			npc.noTileCollide = true;
			npc.knockBackResist = 0f;
			npc.npcSlots = 10;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.takenDamageMultiplier = 0.9f;
			npc.rarity = 5;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
			npc.takenDamageMultiplier = 0.75f;
		}

		int preExisting = 0;
		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC npcScan = Main.npc[i];
				if (npcScan.type != NPCType<Virulent1>() && NPC.downedMoonlord)
                {
					preExisting = 1;
                }
            }
			return SpawnCondition.Dungeon.Chance * 0.0075f * preExisting;
		}

		int phaseTimer = 0;
		int phaseType = 0;
		public override void AI()
		{
			phaseTimer++;
			if (phaseTimer >= 180)
            {
				phaseType++;
				phaseTimer = 0;
			}
			if (phaseType == 0 || phaseType == 1 || phaseType == 3 || phaseType == 4)
			{
				HoverAI();
			}
			if (phaseType == 2)
			{
				DashAI();
			}
			if (phaseType == 5)
			{
				if (npc.life >= npc.lifeMax * 0.667f)
				{
					DashAI();
				}
				else
                {
					SummonAI();
                }
			}
			if (phaseType == 6)
            {
				phaseType = 0;
            }
		}

		int projTimer = 0;
		int flipTimer = 0;
		int flipped = 0;
		public void HoverAI()
		{
			Player player = Main.player[npc.target];
			projTimer++;
			flipTimer++;
			npc.TargetClosest();
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
			if (boltCount >= 6)
			{
				tooManyBolts = true;
			}
			if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && projTimer >= 45 && !tooManyBolts)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<VirulentBolt>(), 0, 0, 0, 0);
				projTimer = 0;
			}
			if (flipTimer >= 90)
			{
				flipped++;
				flipTimer = 0;
			}
			if (flipped >= 4)
            {
				flipped = 0;
            }
			Vector2 homeInOn = player.Center + new Vector2(flipped % 4 * 200 - 300, -250);
			if ((homeInOn - npc.Center).Length() >= 20)
			{
				npc.position += Vector2.Normalize(homeInOn - npc.Center) * (float)Math.Sqrt((homeInOn - npc.Center).Length()) / 2;
			}
			npc.velocity += Vector2.Normalize(homeInOn - npc.Center) / 2;
			if (npc.velocity.Length() >= 10)
            {
				npc.velocity = Vector2.Normalize(npc.velocity) * 10;
            }
		}

		int dashInitiate = 0;
		Vector2 dashOrient = new Vector2(Main.rand.NextBool()? 250 : -250, Main.rand.NextBool()? 250 : -250);
		public void DashAI()
		{
			Player player = Main.player[npc.target];
			Vector2 homeInOn = player.Center + dashOrient;
			dashInitiate++;
			if (dashInitiate < 90)
			{
				if ((homeInOn - npc.Center).Length() >= 20)
				{
					npc.position += Vector2.Normalize(homeInOn - npc.Center) * (float)Math.Sqrt((homeInOn - npc.Center).Length()) / 2;
				}
				npc.velocity += Vector2.Normalize(homeInOn - npc.Center) / 2;
				if (npc.velocity.Length() >= 10)
				{
					npc.velocity = Vector2.Normalize(npc.velocity) * 10;
				}
			}
			if (dashInitiate == 90)
            {
				npc.velocity = Vector2.Normalize(player.Center - npc.Center) * 22.5f;
			}
			else if (dashInitiate > 90)
            {
				npc.velocity /= 1.015f;
			}
			if (dashInitiate >= 180)
			{
				dashOrient = new Vector2(Main.rand.NextBool() ? 250 : -250, Main.rand.NextBool() ? 250 : -250);
				dashInitiate = 0;
			}
		}

		int summonTimer = 0;
		int spectreCount = 0;
		bool tooManySpectres = false;
		public void SummonAI()
        {
			for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC npcScan = Main.npc[i];
				if (npcScan.type == NPCType<Spectre1>() || npcScan.type == NPCType<Spectre2>())
                {
					spectreCount++;
                }
            }
			if (spectreCount >= 5)
            {
				tooManySpectres = true;
            }
			npc.velocity /= 1.02f;
			summonTimer++;
			if (summonTimer % 45 == 1 && !tooManySpectres)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Spectre1>(), 0, 0, 0, 0);
			}
			if (summonTimer >= 180)
			{
				summonTimer = 0;
			}
			spectreCount = 0;
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
			Item.NewItem(npc.getRect(), ItemType<CultPack>(), Main.rand.Next(250, 401));
			Item.NewItem(npc.getRect(), ItemID.SuperHealingPotion, Main.rand.Next(5, 11));
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 2;
			return null;
		}
	}
}
