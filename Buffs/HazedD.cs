using VectorMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class HazedD : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Hadridian Flames");
			Description.SetDefault("Your very soul is being consumed by the flames" +
				"\n-20% damage");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<VectorModPlayer>().HazedD = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<VectorModGlobalNPC>().HazedD = true;
		}
	}
}