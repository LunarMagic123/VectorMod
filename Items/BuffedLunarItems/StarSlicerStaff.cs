using VectorMod.Buffs;
using VectorMod.Projectiles.BuffedLunarItems;
using VectorMod.Items.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.BuffedLunarItems
{
	public class StarSlicerStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Slicer Staff");
			Tooltip.SetDefault("Conjurs an light blue slicer and a heavy gold slicer.");
		}
		public override void SetDefaults()
		{
			item.damage = 52;
			item.width = 48;
			item.height = 48;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.mana = 10;
			item.knockBack = 4;
			item.value = 125000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item15;
			item.autoReuse = true;
			item.noMelee = true;
			item.summon = true;
			item.buffType = BuffType<StarSlicerM>();
			item.shoot = ProjectileType<StarSlicerBlue>();
			item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(360));
			float scale = 0.85f + (Main.rand.NextFloat() * .3f);
			perturbedSpeed *= scale;
			player.AddBuff(item.buffType, 2, true);
			position = Main.MouseWorld;
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			Projectile.NewProjectile(player.Center, perturbedSpeed, type, (int)(damage * 0.75f), knockBack, player.whoAmI);
			Projectile.NewProjectile(player.Center, perturbedSpeed, ProjectileType<StarSlicerBrown>(), damage, knockBack, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StardustCellStaff);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddIngredient(ItemType<Theoplasm>(), 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
