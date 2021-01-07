using VectorMod.Buffs;
using VectorMod.Projectiles.FTProjs;
using VectorMod.Items.CultSet;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace VectorMod.Items
{
	public class VectorModGlobalItem : GlobalItem
	{
		//Hadridian Things
		public bool PlateHusket;
		public bool PlateHood;
		public bool PlateGaze;
		public bool PlateMask;
		public bool RobeHusket;
		public bool RobeHood;
		public bool RobeGaze;
		public bool RobeMask;

		public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
		{
			if (item.type == ItemID.Seed)
            {
				item.value = 2;
            }
		}

		public override GlobalItem Clone(Item item, Item itemClone)
		{
			VectorModGlobalItem myClone = (VectorModGlobalItem)base.Clone(item, itemClone);
			myClone.PlateHusket = PlateHusket;
			myClone.PlateHood = PlateHood;
			myClone.PlateGaze = PlateGaze;
			myClone.PlateMask = PlateMask;
			myClone.RobeHusket = RobeHusket;
			myClone.RobeHood = RobeHood;
			myClone.RobeGaze = RobeGaze;
			myClone.RobeMask = RobeMask;
			return myClone;
		}

		public override string IsArmorSet(Item head, Item body, Item legs)
		{
			if (head.type == ItemType<HadHusket>() && body.type == ItemType<HadPlate>() && legs.type == ItemType<HadTread>())
			{
				return "PlateHusket";
			}
			if (head.type == ItemType<HadHood>() && body.type == ItemType<HadPlate>() && legs.type == ItemType<HadTread>())
			{
				return "PlateHood";
			}
			if (head.type == ItemType<HadGaze>() && body.type == ItemType<HadPlate>() && legs.type == ItemType<HadTread>())
			{
				return "PlateGaze";
			}
			if (head.type == ItemType<HadMask>() && body.type == ItemType<HadPlate>() && legs.type == ItemType<HadTread>())
			{
				return "PlateMask";
			}
			if (head.type == ItemType<HadHusket>() && body.type == ItemType<HadRobe>() && legs.type == ItemType<HadTread>())
			{
				return "RobeHusket";
			}
			if (head.type == ItemType<HadHood>() && body.type == ItemType<HadRobe>() && legs.type == ItemType<HadTread>())
			{
				return "RobeHood";
			}
			if (head.type == ItemType<HadGaze>() && body.type == ItemType<HadRobe>() && legs.type == ItemType<HadTread>())
			{
				return "RobeGaze";
			}
			if (head.type == ItemType<HadMask>() && body.type == ItemType<HadRobe>() && legs.type == ItemType<HadTread>())
			{
				return "RobeMask";
			}

			return base.IsArmorSet(head, body, legs);
        }

        public override void UpdateArmorSet(Player player, string set)
		{
			if (set == "PlateHusket")
			{
				VectorModPlayer.CanPlateHusketB = true;
				player.buffImmune[BuffType<HazedD>()] = true;
				player.setBonus = "Direct melee strikes increase defense and damage reduction" +
					"\nImmunity to 'Hadridian Blaze'";
			}
			if (set == "PlateHood")
			{
				player.buffImmune[BuffType<HazedD>()] = true;
				player.setBonus = "Plate Hood" +
					"\nImmunity to 'Hadridian Blaze'";
			}
			if (set == "PlateGaze")
			{
				player.buffImmune[BuffType<HazedD>()] = true;
				player.setBonus = "Plate Gaze" +
					"\nImmunity to 'Hadridian Blaze'";
			}
			if (set == "PlateMask")
			{
				player.buffImmune[BuffType<HazedD>()] = true;
				player.setBonus = "Increases your maximum minion count by 3" +
					"\nImmunity to 'Hadridian Blaze'";
				player.maxMinions += 3;
			}
			if (set == "RobeHusket")
			{
				player.buffImmune[BuffType<HazedD>()] = true;
				VectorModPlayer.CanRobeHusketB = true;
				player.setBonus = "Direct melee strikes increase life regeneration and movement speed" +
					"\nImmunity to 'Hadridian Blaze'";
			}
			if (set == "RobeHood")
			{
				player.buffImmune[BuffType<HazedD>()] = true;
				player.setBonus = "Robe Hood" +
					"\nImmunity to 'Hadridian Blaze'";
			}
			if (set == "RobeGaze")
			{
				player.buffImmune[BuffType<HazedD>()] = true;
				player.setBonus = "Robe Gaze" +
					"\nImmunity to 'Hadridian Blaze'";
			}
			if (set == "RobeMask")
			{
				player.buffImmune[BuffType<HazedD>()] = true;
				player.minionDamage += 0.48f;
				player.setBonus = "48% increased summon damage" +
					"\nImmunity to 'Hadridian Blaze'";
			}
		}

        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (VectorModPlayer.CanPlateHusketB)
			{
				player.AddBuff(BuffType<PlateHusketB>(), 300);
			}
			if (VectorModPlayer.CanRobeHusketB)
			{
				player.AddBuff(BuffType<RobeHusketB>(), 300);
			}
		}
    }
}