using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items
{
	public class GoldLeaf : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Golden Leaf");
			Tooltip.SetDefault("You can see a funny distorted reflection of youself on the leaf's surface.");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 26;
			item.maxStack = 999;
			item.value = 350;
			item.rare = ItemRarityID.Orange;
		}
	}
}