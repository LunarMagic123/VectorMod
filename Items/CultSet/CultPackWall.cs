using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.CultSet
{
	public class CultPackWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cult Pack Wall");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = 15000;
			item.rare = ItemRarityID.Red;
			item.maxStack = 999;
			item.consumable = true;
			item.createWall = WallType<Tiles.CultTiles.CultPackWallTile>();
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 5;
			item.useStyle = ItemUseStyleID.SwingThrow;
		}
	}
}
