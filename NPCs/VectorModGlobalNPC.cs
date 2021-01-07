using VectorMod.Dusts;
using VectorMod.Items.BuffedLunarItems;
using VectorMod.Items.Willpower;
using VectorMod.Items.CultSet;
using VectorMod.NPCs.CultInvasion;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using VectorMod.Projectiles.BuffedLunarItems;
using Terraria.ID;
using System;

namespace VectorMod.NPCs
{
	public class VectorModGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		//Hadridian Buffs
		public bool HazedD;
		//Lunar
		public bool HellfireD;

		public override void ResetEffects(NPC npc) {
			HazedD = false;
			HellfireD = false;
			npc.buffImmune[BuffType<Buffs.HazedD>()] = false;
		}

		public override void NPCLoot(NPC npc)
		{
			if (NPC.downedBoss1 && Main.bloodMoon && Main.rand.NextBool(20))
			{
				Item.NewItem(npc.getRect(), ItemType<Willpower>(), (int)(Math.Sqrt(npc.lifeMax) * Main.rand.NextFloat(0.75f, 1.25f)));
			}

			if (npc.type != NPCID.DungeonSpirit && npc.type != NPCType<Spectre1>() && npc.type != NPCType<Spectre2>() && Main.player[Main.myPlayer].ZoneDungeon && NPC.downedMoonlord && Main.rand.NextBool(10) && npc.lifeMax >= 300)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Spectre1>(), 0, 0, 0, 0);
			}
			if (Main.player[Main.myPlayer].ZoneDungeon && Main.rand.NextBool() && NPC.downedMoonlord && Main.rand.NextBool((int)Math.Sqrt(npc.lifeMax) * 4))
			{
				Item.NewItem(npc.getRect(), ItemType<CultPack>(), (int)(Math.Sqrt(npc.lifeMax) * Main.rand.NextFloat(0.75f, 1.25f)));
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (HazedD)
			{
				if (Main.rand.NextBool())
				{
					int dust = Dust.NewDust(npc.position - new Vector2(20f, 20f), npc.width + 40, npc.height + 40, DustType<HazeDust>(), npc.velocity.X * 0.8f, npc.velocity.Y * 0.8f, 100, default(Color), 4f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity.X = (npc.Center.X - Main.dust[dust].position.X) / 10;
					Main.dust[dust].velocity.Y = (npc.Center.Y - Main.dust[dust].position.Y) / 10;
				}
				Lighting.AddLight(npc.position, 0.8f, 0.2f, 0f);
			}
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (HazedD) {
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= (int)(480 - Math.Sqrt(npc.defense) * 10);
				if (damage < Math.Abs(npc.lifeRegen / 8))
				{
					damage = Math.Abs(npc.lifeRegen / 8);
				}
			}
			if (HellfireD) {
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				int Sticks = 0;
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile p = Main.projectile[i];
					if (p.active && p.type == ProjectileType<HellfireProj>() && p.ai[0] == 1f && p.ai[1] == npc.whoAmI)
					{
						Sticks++;
					}
				}
				if (Sticks == 0)
                {
					npc.lifeRegen -= 400;
					if (damage < 50)
					{
						damage = 50;
					}
				}
				npc.lifeRegen -= (int)(Sticks * 400 - Math.Sqrt(npc.defense) * Sticks * 10);
				if (damage < Math.Abs(npc.lifeRegen / 8))
				{
					damage = Math.Abs(npc.lifeRegen / 8);
				}

			}
		}

		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			Player player = Main.LocalPlayer;
			if (type == NPCID.Cyborg)
			{
				if (player.CountItem(ItemType<Voidwalker>(), 1) >= 1)
				{
					shop.item[nextSlot].SetDefaults(ItemType<WalkerArrowItem>());
					nextSlot++;
				}
			}
		}
	}
}
