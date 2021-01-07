using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VectorMod.Buffs
{
	public class VectorModGlobalBuff : GlobalBuff
	{
        public override void ModifyBuffTip(int type, ref string tip, ref int rare)
        {
            if (type == BuffID.Swiftness)
            {
                tip = "20% increased movement speed";
            }
        }

        public override void Update(int type, Player player, ref int buffIndex)
        {
            if (player.HasBuff(BuffID.Swiftness))
            {
                player.moveSpeed -= 0.05f;
            }
        }
    }
}