using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Buffs.Willpower
{
    public class VitalityB : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Vitality");
            Description.SetDefault("+6 life regeneration");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 6;
            if (player.HasBuff(ModContent.BuffType<VitalityB>()) && player.HasBuff(BuffID.Regeneration))
            {
                player.ClearBuff(BuffID.Regeneration);
            }
        }
    }
}
