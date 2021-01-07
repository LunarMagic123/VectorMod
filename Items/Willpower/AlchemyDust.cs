using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace VectorMod.Items.Willpower
{
	public class AlchemyDust : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Alchemical Powder");
			Tooltip.SetDefault("A powerful substitute and fertilizer for magical herbs.");
		}
		public override void SetDefaults()
		{
			item.height = 24;
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
			item.value = 150;
			item.width = 32;
		}
	}
}