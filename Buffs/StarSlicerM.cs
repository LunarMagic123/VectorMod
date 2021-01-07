using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class StarSlicerM : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Star Slicers");
			Description.SetDefault("The Star Slicers will fight for you.");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			VectorModPlayer modPlayer = player.GetModPlayer<VectorModPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.BuffedLunarItems.StarSlicerBlue>()] > 0)
			{
				modPlayer.StarSlicerM = true;
			}
			if (!modPlayer.StarSlicerM)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}