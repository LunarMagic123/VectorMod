using VectorMod.Projectiles.BuffedLunarItems;
using VectorMod.Items.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.BuffedLunarItems
{
	public class Voidwalker : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voidwalker");
			Tooltip.SetDefault("66% chance not to consume ammo" +
				"\nNot to be confused with the Phangasm. This weapons is obviously superior!" +
				"\nConverts Wooden Arrows into Walker Arrows" +
				"\nWalker Arrows may be purchased from the Cyborg when this item is in your inventory.");
		}
		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.channel = true;
			item.damage = 70;
			item.height = 44;
			item.knockBack = 4;
			item.rare = ItemRarityID.Red;
			item.ranged = true;
			item.noMelee = true;
			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 30;
			item.useAnimation = 15;
			item.useAmmo = AmmoID.Arrow;
			item.UseSound = SoundID.Item5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 15;
			item.value = 150000;
			item.width = 38;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .66f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ProjectileType<WalkerArrowProj>();
			}
			for (int i = 0; i < 2 + Main.rand.Next(4); i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(6f));
				Projectile.NewProjectile(player.Center, perturbedSpeed, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}

		float voidwalkerSpeed;
		public override float UseTimeMultiplier(Player player)
		{
			return voidwalkerSpeed;
		}

        public override void HoldItem(Player player)
		{
			if (player.channel == true && voidwalkerSpeed <= 1)
			{
				voidwalkerSpeed += 1 / 600f;
			}
			if (player.channel == false)
			{
				voidwalkerSpeed = 0.25f;
			}
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Phantasm);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddIngredient(ItemType<Theoplasm>(), 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}