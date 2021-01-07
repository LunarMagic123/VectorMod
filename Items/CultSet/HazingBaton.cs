using VectorMod.Projectiles.CultSet;
using VectorMod.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.CultSet
{
	public class HazingBaton : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hazing Baton");
			Tooltip.SetDefault("Summons a powerful plated pyramid." +
                "\nPlated Hazers will decrease enemies' defense against other occultic weapons.");
		}
		public override void SetDefaults()
		{
			item.damage = 65;
			item.width = 18;
			item.height = 42;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.mana = 10;
			item.knockBack = 8;
			item.value = 125000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item15;
			item.autoReuse = true;
			item.noMelee = true;
			item.summon = true;
			item.buffType = BuffType<HazerM>();
			item.shoot = ProjectileType<PlateHazerP>();
			item.shootSpeed = 15f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, 2, true);
			position = Main.MouseWorld;
			speedX *= Main.rand.NextFloat(0.8f, 1.2f);
			speedY *= Main.rand.NextFloat(0.8f, 1.2f);
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
