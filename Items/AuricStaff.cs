using VectorMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items
{
	public class AuricStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Auric Staff");
			Tooltip.SetDefault("Spawns a cluster of homing Auric Orbs at the cursor.");
			Item.staff[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.damage = 27;
			item.width = 40;
			item.height = 40;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.mana = 20;
			item.knockBack = 4;
			item.value = 15000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.noMelee = true;
			item.magic = true;
			item.shoot = ProjectileType<AuricOrb>();
			item.shootSpeed = 2f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(5);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(360));
				float scale = 0.85f + (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MagicMissile);
			recipe.AddIngredient(ItemID.GoldBar, 16);
			recipe.AddIngredient(ItemID.FallenStar, 4);
			recipe.AddIngredient(ItemID.MarbleBlock, 48);
			recipe.AddIngredient(ItemType<GoldLeaf>(), 16);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}