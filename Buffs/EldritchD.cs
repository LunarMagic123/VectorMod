using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class EldritchD : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Eldritch Wrath");
			Description.SetDefault("The Eldritch being is siphoning your life force.");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<VectorModPlayer>().EldritchD = true;
		}
	}
}