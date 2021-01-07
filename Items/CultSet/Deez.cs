using VectorMod.Projectiles.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.CultSet
{
	public class Deez : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sacrificial Sunderer");
			Tooltip.SetDefault("Blood for Blood God.");
		}
		public override void SetDefaults()
		{
			item.damage = 110;
			item.width = 56;
			item.height = 40;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 250000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.melee = true;
			item.shoot = ProjectileType<DeezProj>();
			item.shootSpeed = 5f;
			item.noUseGraphic = true;
			item.noMelee = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int NumProjs = 3 + Main.rand.Next(3);
			for (int i = 0; i < NumProjs ; i++)
			{
				float PosX = player.Center.X - 150 + Main.rand.Next(300);
				float PosY = player.Center.Y + 150 - Main.rand.Next(150);
				Projectile.NewProjectile(PosX, PosY, 0, -5, ProjectileType<DeezProj>(), damage, knockBack, player.whoAmI);
			}
			return false;
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
}
