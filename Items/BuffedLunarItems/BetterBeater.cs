using VectorMod.Items.CultSet;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace VectorMod.Items.BuffedLunarItems
{
	public class BetterBeater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kinetic Disintegrator");
			Tooltip.SetDefault("66% chance not to consume ammo" +
				"\nIt's a boy!");
		}
		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.damage = 180;
			item.height = 30;
			item.knockBack = 3;
			item.rare = ItemRarityID.Red;
			item.ranged = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shoot = ProjectileID.Bullet;
			item.shootSpeed = 25;
			item.useAmmo = AmmoID.Bullet;
			item.useAnimation = 5;
			item.UseSound = SoundID.Item40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 5;
			item.value = 150000;
			item.width = 80;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .66f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(1));
			float scale = 0.8f + (Main.rand.NextFloat() * .4f);
			perturbedSpeed *= scale;
			Projectile.NewProjectile(position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
			return false;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.VortexBeater);
			recipe.AddIngredient(ItemID.LunarBar, 86);
			recipe.AddIngredient(ModContent.ItemType<Theoplasm>(), 18);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}