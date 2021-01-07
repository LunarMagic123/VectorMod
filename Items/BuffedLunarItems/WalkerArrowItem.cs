using VectorMod.Projectiles.BuffedLunarItems;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.BuffedLunarItems
{
	public class WalkerArrowItem : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Walker Arrow");
		}

		public override void SetDefaults() {
			item.damage = 20;
			item.ranged = true;
			item.width = 22;
			item.height = 38;
			item.maxStack = 999;
			item.consumable = true;
			item.value = 5000;
			item.rare = ItemRarityID.Cyan;
			item.shoot = ProjectileType<WalkerArrowProj>();
			item.shootSpeed = 30f;
			item.ammo = AmmoID.Arrow;
		}
	}
}
