using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items.CultSet
{
	[AutoloadEquip(EquipType.Body)]
	public class HadPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hadridian Chestplate");
			Tooltip.SetDefault("25% reduced movement speed" +
				"\n10% increased maximum life and mana" +
				"\nIncreases the effectiveness of damage reduction by 33%" +
				"\nFlavour Text");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 125000;
			item.rare = ItemRarityID.Red;
			item.defense = 42;
		}

		public override void UpdateEquip(Player player) {
			player.endurance *= 1.33f;
			player.moveSpeed -= 0.25f;
			player.statLifeMax2 = (int)(player.statLifeMax2 * 1.1f);
			player.statManaMax2 = (int)(player.statManaMax2 * 1.1f);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "CultPack", 240);
			recipe.AddIngredient(mod, "Theoplasm", 24);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}