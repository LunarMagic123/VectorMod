using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items.CultSet
{
	[AutoloadEquip(EquipType.Body)]
	public class HadRobe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hadridian Robes");
			Tooltip.SetDefault("20% increased damage" +
				"\nReduces the effectiveness of damage reduction by 33%" +
				"\nThe arcane fabric melds with the wearer, making them a powerful conduit for magic.");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 125000;
			item.rare = ItemRarityID.Red;
			item.defense = 12;
		}

		public override void UpdateEquip(Player player) {
			player.allDamage += 0.2f;
			player.endurance /= 1.33f;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms) {
			drawHands = true;
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
