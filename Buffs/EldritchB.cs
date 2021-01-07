using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class EldritchB : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Eldritch Rage");
			Description.SetDefault("The Eldritch being shares its siphoned life force with you.");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<VectorModPlayer>().EldritchB = true;
		}
	}
}