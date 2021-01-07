using VectorMod.Projectiles.BuffedLunarItems;
using VectorMod.Items.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace VectorMod.Items.BuffedLunarItems
{
	public class PowerGlove : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Glove");
			Tooltip.SetDefault("'Now you're playing with power!'");
		}
		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.damage = 115;
			item.height = 42;
			item.knockBack = 3;
			item.rare = ItemRarityID.Red;
			item.magic = true;
			item.mana = 12;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<PowerProj>();
			item.shootSpeed = 18;
			item.useAnimation = 18;
			item.noUseGraphic = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 18;
			item.value = 150000;
			item.width = 42;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!Main.rand.NextBool(15))
			{
				for (int i = 0; i < 1 + Main.rand.Next(4); i++)
				{
					Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)), type, damage, knockBack, player.whoAmI);
				}
			}
			else
			{
				Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)) * 1.5f, ModContent.ProjectileType<PowererProj>(), damage * 4, knockBack * 3, player.whoAmI);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.NebulaBlaze);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddIngredient(ModContent.ItemType<Theoplasm>(), 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}