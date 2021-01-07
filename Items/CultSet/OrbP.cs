using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace VectorMod.Items.CultSet
{
	public class OrbP : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Core of Life");
			Tooltip.SetDefault("A great energy is stored within this orb. It's also pretty to look at!");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 6));
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.consumable = true;
			item.damage = 33;
			item.height = 22;
			item.maxStack = 999;
			item.rare = ItemRarityID.Lime;
			item.ranged = true;
			item.value = 20000;
			item.width = 24;
			item.ammo = ItemID.Gel;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Willpower.Willpower>(), 15);
			recipe.AddIngredient(ItemID.SoulofLight);
			recipe.AddIngredient(ItemID.SoulofFlight);
			recipe.AddIngredient(ItemID.SoulofSight);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
		}
	}
}
