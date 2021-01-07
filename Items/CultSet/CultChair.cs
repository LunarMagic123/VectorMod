using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.CultSet
{
	public class CultChair : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cult Chair");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 30000;
			item.rare = ItemRarityID.Red;
			item.maxStack = 99;
			item.consumable = true;
			item.createTile = TileType<Tiles.CultTiles.CultChair>();
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 5;
			item.useStyle = ItemUseStyleID.SwingThrow;
		}
	}
}
