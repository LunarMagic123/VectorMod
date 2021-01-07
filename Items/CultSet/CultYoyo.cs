/*using VectorMod.Projectiles.CultSet;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.CultSet
{
	public class CultYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hazer");
			Tooltip.SetDefault("Very effective at persuading subjects to join the cult.");
		}
		public override void SetDefaults()
		{
			item.channel = true;
			item.damage = 260;
			item.width = 56;
			item.height = 40;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = 250000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.melee = true;
			item.shoot = ProjectileType<CultYoyoFertile>();
			item.shootSpeed = 25f;
			item.noUseGraphic = true;
			item.noMelee = true;
		}

        public override bool CanUseItem(Player player)
        {
			return player.ownedProjectileCounts[ProjectileType<CultYoyoFertile>()] == 0;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "CultPack", 180);
			recipe.AddIngredient(mod, "Theoplasm", 18);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}*/