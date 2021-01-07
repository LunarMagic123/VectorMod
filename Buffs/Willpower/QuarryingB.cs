using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Buffs.Willpower
{
    public class QuarryingB : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Quarrying");
            Description.SetDefault("+50% mining speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.pickSpeed /= 1.5f;
            if (player.HasBuff(ModContent.BuffType<QuarryingB>()) && player.HasBuff(BuffID.Mining))
            {
                player.ClearBuff(BuffID.Mining);
            }
        }
    }
}
