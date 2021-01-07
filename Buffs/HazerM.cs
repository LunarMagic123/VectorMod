using Terraria;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class HazerM : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Hadridian Hazer");
			Description.SetDefault("The Hadridian Hazer will uhh... haze for you I guess?");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			VectorModPlayer modPlayer = player.GetModPlayer<VectorModPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.CultSet.PlateHazerP>()] > 0)
			{
				modPlayer.HazerM = true;
			}
			if (!modPlayer.HazerM)
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