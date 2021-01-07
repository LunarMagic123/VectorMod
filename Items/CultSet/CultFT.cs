using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items.CultSet
{
	public class CultFT : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skull of Sin");
			Tooltip.SetDefault("Spews out soul flames." +
				"\n75% chance not to consume ammo");
		}
		public override void SetDefaults()
		{
			item.damage = 220;
			item.width = 34;
			item.height = 48;
			item.useTime = 9;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4;
			item.value = 250000;
			item.rare = ItemRarityID.Purple;
			item.autoReuse = true;
			item.noMelee = true;
			item.ranged = true;
			item.shootSpeed = 10;
			item.shoot = ModContent.ProjectileType<Projectiles.FTProjs.SinFlame>();
			item.useAmmo = AmmoID.Gel;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/CultFTNoise");
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= 0.75f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
			float scale = 0.9f + (Main.rand.NextFloat() * .2f);
			perturbedSpeed = perturbedSpeed * scale;
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
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
