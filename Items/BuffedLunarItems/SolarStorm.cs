using VectorMod.Projectiles.BuffedLunarItems;
using VectorMod.Items.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.BuffedLunarItems
{
	public class SolarStorm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Storm");
			Tooltip.SetDefault("The culmination of sun and moon. Immense is its potential.");
		}
		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.damage = 55;
			item.height = 44;
			item.knockBack = 6;
			item.rare = ItemRarityID.Red;
			item.melee = true;
			item.noMelee = true;
			item.shoot = ProjectileType<SolarStormTip>();
			item.shootSpeed = 1;
			item.useAnimation = 20;
			item.noUseGraphic = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 20;
			item.value = 150000;
			item.width = 38;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(45));
			Projectile.NewProjectile(player.Center, perturbedSpeed * 36, type, damage, 5, player.whoAmI);
			Projectile.NewProjectile(player.Center, perturbedSpeed, ProjectileType<SolarStormHandle>(), 0, 0, player.whoAmI);
			for (int i = 1; i < 19; i++)
			{
				Projectile.NewProjectile(player.Center, perturbedSpeed * i * 1.8f, ProjectileType<SolarStormChainSmall>(), 0, 3, player.whoAmI);
				Projectile.NewProjectile(player.Center, perturbedSpeed * i * 1.8f + Vector2.Normalize(perturbedSpeed) * 1.8f, ProjectileType<SolarStormChainBig>(), damage, 4, player.whoAmI);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SolarEruption);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddIngredient(ItemType<Theoplasm>(), 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}