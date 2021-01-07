using VectorMod.Projectiles.CultSet;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.CultSet
{
	public class CultStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellstorm Scepter");
			Tooltip.SetDefault("Incredibly efficient in its conversion of matter to energy." +
				"\nConsumes all of the player's mana on use." +
				"\nOnly one bolt may exist at a time. Luckily, the laws of physics are clientsided so your friends can still use it!");
			Item.staff[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.mana = 120;
			item.damage = 330;
			item.width = 28;
			item.height = 30;
			item.useTime = 60;
			item.useAnimation = 60;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5;
			item.value = 250000;
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item73;
			item.autoReuse = true;
			item.noMelee = true;
			item.magic = true;
			item.shoot = ProjectileType<CultBolt>();
			item.shootSpeed = 30f;
		}

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffID.ManaSickness) && player.ownedProjectileCounts[ProjectileType<CultBolt>()] == 0;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "CultPack", 120);
			recipe.AddIngredient(mod, "Theoplasm", 12);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
