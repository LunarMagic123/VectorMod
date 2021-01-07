using VectorMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items
{
	public class AuricTome : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilded Tome");
			Tooltip.SetDefault("Spews golden pages at high speeds.");
		}
		public override void SetDefaults()
		{
			item.damage = 18;
			item.width = 40;
			item.height = 40;
			item.useTime = 6;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.mana = 8;
			item.knockBack = 2;
			item.value = 15000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.noMelee = true;
			item.magic = true;
			item.shoot = ProjectileType<GoldPage>();
			item.shootSpeed = 15f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 1 + Main.rand.Next(3);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
				Projectile.NewProjectile(position, perturbedSpeed * Main.rand.NextFloat(0.75f, 1.25f), type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.AquaScepter);
			recipe.AddIngredient(ItemID.GoldBar, 16);
			recipe.AddIngredient(ItemID.FallenStar, 4);
			recipe.AddIngredient(ItemID.MarbleBlock, 48);
			recipe.AddIngredient(ItemType<GoldLeaf>(), 16);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}