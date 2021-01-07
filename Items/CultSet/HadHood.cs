using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items.CultSet
{
	[AutoloadEquip(EquipType.Head)]
	public class HadHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hadridian Hood");
			Tooltip.SetDefault("16% increased ranged damage" +
				"\n32% increased ranged critical strike chance" +
				"\nFlavour Text.");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 75000;
			item.rare = ItemRarityID.Red;
			item.defense = 24;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 32;
			player.rangedDamage += 0.16f;
		}

        public override bool DrawHead()
        {
			return false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "CultPack", 120);
			recipe.AddIngredient(mod, "Theoplasm", 12);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}