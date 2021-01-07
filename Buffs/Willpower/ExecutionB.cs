using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Buffs.Willpower
{
    public class ExecutionB : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Executioner's  Wrath");
            Description.SetDefault("+16% damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += 0.16f;
            if (player.HasBuff(ModContent.BuffType<ExecutionB>()) && player.HasBuff(BuffID.Wrath))
            {
                player.ClearBuff(BuffID.Wrath);
            }
        }
    }
}
