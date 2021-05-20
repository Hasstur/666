using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace MORGEN.NPCs
{
	public class SquaretronBody : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
		}
		public override void SetDefaults()
		{
			npc.width = 180;
			npc.height = 160;
			npc.aiStyle = -1;
			Main.npcFrameCount[npc.type] = 1;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.damage = 66;
			npc.defense = 0;
			npc.lifeMax = 1;
			npc.dontTakeDamage = true;
			npc.knockBackResist = 0f;
		}
		bool headSpawned = false;
		public override void AI()
		{
			npc.ai[0]++;
			npc.TargetClosest(true);
			if (Main.player[npc.target].dead)
			{
				npc.ai[0] = 0;
				npc.velocity.Y = npc.velocity.Y - 0.4f;
				if (npc.timeLeft > 10 && Main.player[npc.target].dead)
				{
					npc.timeLeft = 10;
					return;
				}
			}
			if (npc.ai[0] >= 360)
			{
				npc.ai[0] = 0;
			}

			NPCGlobal.Body = npc.whoAmI;

			if (Main.player[npc.target].position.X < npc.position.X)
			{
				if (npc.velocity.X > -6) { npc.velocity.X -= 0.2f; }
			}
			else if (Main.player[npc.target].Center.X > npc.Center.X)
			{
				if (npc.velocity.X < 6) { npc.velocity.X += 0.2f; }
			}
			if (Main.player[npc.target].position.Y - 200 < npc.position.Y)
			{
				if (npc.velocity.Y > -6) npc.velocity.Y -= 0.2f;
			}
			else if (Main.player[npc.target].Center.Y - 200 > npc.Center.Y)
			{
				if (npc.velocity.Y < 6) npc.velocity.Y += 0.2f;
			}
			if (!headSpawned)
			{
				NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.GetNPC("SquaretronHead").npc.type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)((double)npc.position.X + (npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("SquaretronHand"), 0, -1, npc.whoAmI);
				NPC.NewNPC((int)((double)npc.position.X + (npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("SquaretronHand2"), 0, 1, npc.whoAmI);
				headSpawned = true;
			}
		}
	}
}
