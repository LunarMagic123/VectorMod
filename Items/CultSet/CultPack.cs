using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.CultSet
{
	public class CultPack : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cult Pack");
			Tooltip.SetDefault("Filled with all sorts of occultic goodies!");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 15000;
			item.rare = ItemRarityID.Red;
			item.maxStack = 999;
			item.consumable = true;
			item.createTile = TileType<Tiles.CultTiles.CultPackTile>();
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 5;
			item.useStyle = ItemUseStyleID.SwingThrow;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.SetResult(ItemType<CultPackWall>(), 4);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CultPackWall>(), 4);
			recipe.SetResult(this);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 4);
			recipe.SetResult(ItemType<CultChair>());
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 8);
			recipe.SetResult(ItemType<CultTable>());
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 6);
			recipe.SetResult(ItemType<CultDoor>());
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 6);
			recipe.AddIngredient(ItemID.Torch);
			recipe.SetResult(ItemType<CultLantern>());
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
		}
	}
}
