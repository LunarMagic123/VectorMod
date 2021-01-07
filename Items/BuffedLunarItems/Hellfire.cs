using VectorMod.Projectiles.BuffedLunarItems;
using VectorMod.Items.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items.BuffedLunarItems
{
	public class Hellfire : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellfire");
			Tooltip.SetDefault("You might feel a slight burning sensation.");
		}
		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.damage = 80;
			item.height = 42;
			item.knockBack = 12;
			item.rare = ItemRarityID.Red;
			item.melee = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<HellfireProj>();
			item.shootSpeed = 35;
			item.useAnimation = 30;
			item.noUseGraphic = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 30;
			item.value = 150000;
			item.width = 42;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < Main.rand.Next(2, 6); i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
				Projectile.NewProjectile(player.Center, perturbedSpeed * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockBack, player.whoAmI);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DayBreak);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddIngredient(ModContent.ItemType<Theoplasm>(), 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}