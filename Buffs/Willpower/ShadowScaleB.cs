using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Buffs.Willpower
{
    public class ShadowScaleB : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shadow Scale Skin");
            Description.SetDefault("+12 defense");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 12;
            if (player.HasBuff(ModContent.BuffType<ShadowScaleB>()) && player.HasBuff(BuffID.Ironskin))
            {
                player.ClearBuff(BuffID.Ironskin);
            }
        }
    }
}
