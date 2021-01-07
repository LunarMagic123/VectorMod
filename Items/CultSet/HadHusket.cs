using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items.CultSet
{
	[AutoloadEquip(EquipType.Head)]
	public class HadHusket : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hadridian Husket");
			Tooltip.SetDefault("32% increased melee damage" +
				"\n16% increased melee critical strike chance" +
				"\nFlavour Text.");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 75000;
			item.rare = ItemRarityID.Red;
			item.defense = 36;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeCrit += 16;
			player.meleeDamage += 0.32f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "CultPack", 120);
			recipe.AddIngredient(mod, "Theoplasm", 12);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}