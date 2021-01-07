using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class PlateHusketB : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Valiant Endurance");
			Description.SetDefault("+12 defense and +6% damage reduction");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<VectorModPlayer>().PlateHusketB = true;
		}
	}
}