using VectorMod.Projectiles.CultSet;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.CultSet
{
	public class Theoplasm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Theoplasm");
			Tooltip.SetDefault("The source of the cult's unearthly energy.");
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.consumable = true;
			item.damage = 70;
			item.height = 38;
			item.maxStack = 999;
			item.rare = ItemRarityID.Red;
			item.value = 15000;
			item.width = 40;
			item.shoot = ProjectileType<DeezProj>();
			item.ammo = ItemID.Gel;
		}
	}
}
