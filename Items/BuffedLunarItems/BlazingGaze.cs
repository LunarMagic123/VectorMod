using VectorMod.Projectiles.BuffedLunarItems;
using VectorMod.Items.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace VectorMod.Items.BuffedLunarItems
{
	public class BlazingGaze : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Gaze");
			Tooltip.SetDefault("Summons an eldritch being that siphons the surrounding lifeforce and redirects a portion to you." +
				"\nHowever, it does not discriminate against what or who it drains the lifeforce of.");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.damage = 110;
			item.height = 36;
			item.rare = ItemRarityID.Red;
			item.magic = true;
			item.mana = 150;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<BlazingGazeProj>();
			item.shootSpeed = 10;
			item.useAnimation = 90;
			item.noUseGraphic = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 90;
			item.value = 150000;
			item.width = 22;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
			Projectile.NewProjectile(player.Center, perturbedSpeed, ModContent.ProjectileType<Blaz2>(), damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(player.Center, perturbedSpeed, ModContent.ProjectileType<Blaz1>(), damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(player.Center - new Vector2(0, 5), perturbedSpeed, type, damage, knockBack, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.NebulaArcanum);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddIngredient(ModContent.ItemType<Theoplasm>(), 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}