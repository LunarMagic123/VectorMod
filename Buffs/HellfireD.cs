using VectorMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class HellfireD : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Die");
			Description.SetDefault("CHANLER");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<VectorModGlobalNPC>().HellfireD = true;
		}
	}
}