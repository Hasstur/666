using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MORGEN
{
	public class NPCGlobal : GlobalNPC
	{
		public static int Boss = -1;
		public static int Body = -1;
		public static int handright = -1;
		public static int handleft = -1;

		public override void HitEffect(NPC npc, int hitDirection, double damage)
		{
			Player player = Main.player[Main.myPlayer];
			if (player.GetModPlayer<MPlayer>().cashback)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SilverCoin, Main.rand.Next(1, 20));
				player.GetModPlayer<MPlayer>().cashback = false;
			}
		}
		public override void NPCLoot(NPC npc)
		{
			if (npc.type == NPCID.RedDevil && Main.rand.NextFloat(6.66f) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SnusPack"));
			}
		}
	}
}