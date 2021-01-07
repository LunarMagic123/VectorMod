using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Items {
	[AutoloadEquip(EquipType.Shoes)]
	public class GildedDash : ModItem {
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Gilded Dash");
			Tooltip.SetDefault("Allows the player to dash by double tapping left, right, or up.");
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults() {
			item.width = 24;
			item.height = 32;
			item.defense = 2;
			item.accessory = true;
			item.rare = ItemRarityID.Orange;
			item.value = 8000;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			GDash mp = player.GetModPlayer<GDash>();

			if (!mp.DashActive)
				return;

			player.eocDash = mp.DashTimer;
			player.armorEffectDrawShadowEOCShield = true;

			if (mp.DashTimer == GDash.Duration) {
				Vector2 newVelocity = player.velocity;

				if (mp.DashDir == GDash.DashUp && player.velocity.Y > -mp.DashVelocity) {
					newVelocity.Y = -10;
				}
				else if ((mp.DashDir == GDash.DashLeft && player.velocity.X > -mp.DashVelocity) || (mp.DashDir == GDash.DashRight && player.velocity.X < mp.DashVelocity)) {
					newVelocity.X = mp.DashDir == GDash.DashRight ? 12.5f : -12.5f;
				}

				player.velocity = newVelocity;
			}

			mp.DashTimer--;
			mp.DashDelay--;

			if (mp.DashTimer == 0) {
				if ((mp.DashDir == GDash.DashLeft) || (mp.DashDir == GDash.DashRight)) {
					player.velocity.X *= 0.75f;
				}
				if ((mp.DashDir == GDash.DashUp) || (mp.DashDir == GDash.DashDown)) {
					player.velocity.Y *= 0.75f;
				}
			}

			if (mp.DashDelay == 0) {
				mp.DashDelay = GDash.Downtime;
				mp.DashTimer = GDash.Duration;
				mp.DashActive = false;
			}
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GoldLeaf>(), 32);
			recipe.AddIngredient(ItemID.Bone, 48);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	public class GDash : ModPlayer
	{
		public static readonly int DashDown = 0;
		public static readonly int DashUp = 1;
		public static readonly int DashRight = 2;
		public static readonly int DashLeft = 3;
		public int DashDir = -1;
		public bool DashActive = false;
		public int DashDelay = Downtime;
		public int DashTimer = Duration;
		public readonly float DashVelocity = 15f;
		public static readonly int Downtime = 50;
		public static readonly int Duration = 30;

		public override void ResetEffects() {
			bool dashAccessoryEquipped = false;

			for(int i = 3; i < 8 + player.extraAccessorySlots; i++) {
				Item item = player.armor[i];

				if(item.type == ModContent.ItemType<GildedDash>())
					dashAccessoryEquipped = true;
				else if(item.type == ItemID.EoCShield || item.type == ItemID.MasterNinjaGear || item.type == ItemID.Tabi)
					return;
			}

			if(!dashAccessoryEquipped || player.setSolar || player.mount.Active || DashActive)
				return;

			else if(player.controlUp && player.releaseUp && player.doubleTapCardinalTimer[DashUp] < 15)
				DashDir = DashUp;
			else if(player.controlRight && player.releaseRight && player.doubleTapCardinalTimer[DashRight] < 15)
				DashDir = DashRight;
			else if(player.controlLeft && player.releaseLeft && player.doubleTapCardinalTimer[DashLeft] < 15)
				DashDir = DashLeft;
			else
				return;

			DashActive = true;

			float speedX = 0f;
			float speedY = 0f;
			for (int i = 0; i < 15; i++) {
				int dustIndex2 = Dust.NewDust(player.Center, 16, 24, 7, speedX, speedY, 0, default(Color), 2f);
				Main.dust[dustIndex2].velocity *= 1.5f;
			}
		}
	}
}
