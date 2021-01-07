using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Buffs.Willpower
{
    public class RampageB : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Rampage");
            Description.SetDefault("+16% critical strike chance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeCrit += 16;
            player.rangedCrit += 16;
            player.magicCrit += 16;
            if (player.HasBuff(ModContent.BuffType<RampageB>()) && player.HasBuff(BuffID.Rage))
            {
                player.ClearBuff(BuffID.Rage);
            }
        }
    }
}
