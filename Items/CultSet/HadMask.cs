using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items.CultSet
{
	[AutoloadEquip(EquipType.Head)]
	public class HadMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hadridian Mask");
			Tooltip.SetDefault("48% increased summon damage" +
				"\nIncreases your maximum minion count by 2" +
				"\nFlavour Text.");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 75000;
			item.rare = ItemRarityID.Red;
			item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions += 3;
			player.minionDamage += 0.48f;
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