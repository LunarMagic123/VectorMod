using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.NPCs.CultInvasion
{
	public class VirulentBolt : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Virulent Bolt");
		}

		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.damage = 60;
			npc.height = 24;
			npc.hide = true;
			npc.knockBackResist = 0;
			npc.lavaImmune = true;
			npc.lifeMax = 100;
			npc.noGravity = true;
			npc.width = 24;
		}

		int turnTimer = 0;
		public override void AI()
		{
			npc.TargetClosest();
			Player player = Main.player[npc.target];
			turnTimer++;
			if (turnTimer >= 8)
            {
				npc.velocity = Vector2.Normalize(player.Center - npc.Center).RotatedByRandom(2) * 7.5f;
				turnTimer = 0;
			}
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Main.dust[Dust.NewDust(npc.position, 24, 24, ModContent.DustType<Dusts.HazeDust>(), 0f, 0f, 0, default, 1.5f)];
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.velocity = npc.velocity / 5;
			}

			Lighting.AddLight(npc.Center, 0.8f, 0.2f, 0.0f);
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			npc.life = 0;
			target.AddBuff(ModContent.BuffType<Buffs.HazedD>(), 180);
        }
    }
}
