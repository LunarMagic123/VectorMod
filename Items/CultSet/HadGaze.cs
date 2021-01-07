using VectorMod.Projectiles.CultSet;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace VectorMod.Items.CultSet
{
	[AutoloadEquip(EquipType.Head)]
	public class HadGaze : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hadridian Gaze");
			Tooltip.SetDefault("24% increased magic damage and critical strike chance" +
				"\nIncreased mana capacity and regeneration" +
				"\nYour eye fires out Hadridian Tentacles. The frequency and power of this attack increases as your health decreases." +
				"\nFlavor Text.");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

        public override void SetDefaults()
		{
			item.width = 16;
			item.height = 26;
			item.value = 75000;
			item.rare = ItemRarityID.Red;
			item.defense = 14;
		}

		public override void UpdateEquip(Player player)
		{
			VectorModPlayer.CanGaze = true;
			player.magicCrit += 24;
			player.magicDamage += 0.24f;
			player.manaRegen += 5;
			player.statManaMax2 += 60;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HadGazeProj>()] == 0)
			{
				Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<HadGazeProj>(), 0, 0, player.whoAmI);
			}
		}

		public override bool DrawHead()
		{
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "CultPack", 160);
			recipe.AddIngredient(mod, "Theoplasm", 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}