using VectorMod.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace VectorMod
{
	class GlobalNPCStuff : GlobalNPC
	{
		public override void NPCLoot(NPC npc)
		{
			if (npc.type == NPCID.GreekSkeleton)
			{
				if (Main.rand.NextBool())
					Item.NewItem(npc.getRect(), ModContent.ItemType<GoldLeaf>(), 3 + Main.rand.Next(3));
			}
			if (npc.type == NPCID.Medusa)
			{
				if (Main.rand.Next(3) <= 3)
					Item.NewItem(npc.getRect(), ModContent.ItemType<GoldLeaf>(), 8 + Main.rand.Next(7));
			}
		}
	}
	public class VectorMod : Mod {
		public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Mushroom", new int[]
			{
				ModContent.ItemType<ShroomGroupIcon>(),
				ItemID.VileMushroom,
				ItemID.ViciousMushroom
			});
			RecipeGroup.RegisterGroup("VectorMod:EvilShrooms", group);
		}
	}
}