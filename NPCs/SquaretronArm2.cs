using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace MORGEN.NPCs
{
	public class SquaretronArm2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
		}
		public override void SetDefaults()
		{
			npc.lifeMax = 1;
			npc.knockBackResist = 0.5f;
			npc.width = 100;
			npc.height = 24;
			npc.aiStyle = 0;
			Main.npcFrameCount[npc.type] = 1;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.dontTakeDamage = true;
		}		
		public static Vector2 CenterPoint(Vector2 A, Vector2 B)
		{
			return new Vector2((A.X + B.X) / 2.0f, (A.Y + B.Y) / 2f);
		}	
		public static float rotateBetween2Points(Vector2 A, Vector2 B)
		{
			return (float)Math.Atan2(A.Y - B.Y, A.X - B.X);
		}
		public override void AI()
		{
			if ((Main.npc[(int)npc.ai[0]].type == mod.NPCType("SquaretronHand") || Main.npc[(int)npc.ai[0]].type == mod.NPCType("SquaretronHand2")) && Main.npc[(int)npc.ai[0]].active)
			{
				npc.Center = CenterPoint(CenterPoint(Main.npc[(int)npc.ai[3]].Center, Main.npc[(int)npc.ai[0]].Center), Main.npc[(int)npc.ai[0]].Center);
				npc.rotation = rotateBetween2Points(Main.npc[(int)npc.ai[3]].Center, Main.npc[(int)npc.ai[0]].Center);
			}
			else
				npc.life = -1;
		}		
	}
}