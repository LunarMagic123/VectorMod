using VectorMod.Items.CultSet;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace VectorMod.NPCs.CultInvasion
{
	public class Spectre2 : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Lesser Occultic Spectre");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults() {
			npc.width = 20;
			npc.height = 36;
			npc.aiStyle = -1;
			npc.damage = 150;
			npc.lifeMax = 6250;
			npc.HitSound = SoundID.NPCHit36;
			npc.DeathSound = SoundID.NPCDeath39;
			npc.value = 750;
			npc.noTileCollide = true;
			npc.knockBackResist = 0.5f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.takenDamageMultiplier = 5;
		}

		bool SCATTER = true;
        public override void AI()
        {
			if (SCATTER)
            {
				npc.velocity = new Microsoft.Xna.Framework.Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10));
				SCATTER = false;
            }

			Player player = Main.player[npc.target];
			npc.TargetClosest();
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
			if (frameTimer >= 6)
            {
				frameTimer = 0;
            }
			npc.frame.Y = frameTimer * frameHeight;
		}

		bool canDrop = true;
		public override void NPCLoot()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npcScan = Main.npc[i];
				if (npcScan.type == NPCType<Virulent1>())
				{
					canDrop = false;
				}
			}
			if (canDrop)
			{
				Item.NewItem(npc.getRect(), ItemType<Theoplasm>(), Main.rand.Next(1, 3));
			}
		}
	}
}
