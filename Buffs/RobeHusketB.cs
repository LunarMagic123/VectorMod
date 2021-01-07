using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class RobeHusketB : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Valiant Vitality");
			Description.SetDefault("+6 life regen and +12% movement speed");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<VectorModPlayer>().RobeHusketB = true;
		}
	}
}