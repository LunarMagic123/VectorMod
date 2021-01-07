using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Buffs.Willpower
{
    public class TurboB : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Turbo Speed!");
            Description.SetDefault("+40% movement speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.4f;
            if (player.HasBuff(ModContent.BuffType<TurboB>()) && player.HasBuff(BuffID.Swiftness))
            {
                player.ClearBuff(BuffID.Swiftness);
            }
        }
    }
}
