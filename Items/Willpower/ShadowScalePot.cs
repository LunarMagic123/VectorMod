using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items.Willpower
{
	public class ShadowScalePot : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Scale Potion");
            Tooltip.SetDefault("Increases defense by 12.");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 28;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = ItemRarityID.Pink;
            item.value = 5000;
            item.buffType = BuffType<Buffs.Willpower.ShadowScaleB>();
            item.buffTime = 57600;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronskinPotion);
            recipe.AddIngredient(ItemType<AlchemyDust>(), 3);
            recipe.AddIngredient(ItemType<CultSet.OrbG>());
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronskinPotion);
            recipe.AddIngredient(ItemType<AlchemyDust>(), 3);
            recipe.AddIngredient(ItemType<CultSet.OrbP>());
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
