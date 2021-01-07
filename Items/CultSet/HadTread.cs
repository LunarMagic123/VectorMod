using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items.CultSet
{
	[AutoloadEquip(EquipType.Legs)]
	public class HadTread : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hadridian Treads");
			Tooltip.SetDefault("25% increased movement speed" +
				"\nMarks the dirt beneath it with the sign of the cult. Tread lightly if you wish not to attract them.");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 100000;
			item.rare = ItemRarityID.Red;
			item.defense = 14;
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += 0.25f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "CultPack", 160);
			recipe.AddIngredient(mod, "Theoplasm", 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}