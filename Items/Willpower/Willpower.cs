using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace VectorMod.Items.Willpower
{
	public class Willpower : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Willpower");
			Tooltip.SetDefault("Only the strongest of wills may exist without an owner.");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 3));
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
			item.value = 25;
			item.width = 20;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 4);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(ItemID.FallenStar);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 6);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(ModContent.ItemType<AlchemyDust>());
			recipe.AddRecipe();
		}
	}
}