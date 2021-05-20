using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace MORGEN.NPCs
{
	public class SquaretronHand : ModNPC
	{
		private const float maxSpeed = 8f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Squaretron Hand");
		}
		public override void SetDefaults()
		{
			npc.width = 50;
			npc.height = 90;
			npc.aiStyle = -1;
			Main.npcFrameCount[npc.type] = 1;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.damage = 66;
			npc.defense = 0;
			npc.lifeMax = 1;
			npc.dontTakeDamage = true;
			npc.knockBackResist = 0f;
			npc.DeathSound = SoundID.NPCDeath1;
		}
		private void IdleBehavior()
		{
			Vector2 target = Main.npc[NPCGlobal.Boss].Bottom + new Vector2(-320f, 35f);
			Vector2 change = target - npc.Bottom;
			CapVelocity(ref change, maxSpeed * 2.5f);
			ModifyVelocity(change);
			CapVelocity(ref npc.velocity, maxSpeed * 2.5f);
		}
		private void ModifyVelocity(Vector2 modify, float weight = 0.05f)
		{
			npc.velocity = Vector2.Lerp(npc.velocity, modify, weight);
		}
		private void CapVelocity(ref Vector2 velocity, float max)
		{
			if (velocity.Length() > max)
			{
				velocity.Normalize();
				velocity *= max;
			}
		}
		private void MakeArms()
		{
			int arm = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SquaretronArm"), 0, 9999, 1, 1, npc.ai[1]);
			int arm2 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SquaretronArm2"), 0, npc.whoAmI, 0, 1, arm);
			Main.npc[arm].ai[0] = arm2;
		}
		bool shoot = false;
		bool spawn = false;

		public override void AI()
		{
			if (!spawn)
			{
				spawn = true;
				MakeArms();
			}
			NPCGlobal.handleft = npc.whoAmI;
			if (!NPC.AnyNPCs(mod.NPCType("SquaretronBody")))
			{
				npc.active = false;
			}
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(true);
			}
			if (Main.npc[NPCGlobal.Body].ai[0] <= 30 || Main.npc[NPCGlobal.Body].ai[0] >= 80)
			{
				IdleBehavior();
			}

			int projType = ProjectileID.Bone;
			int proDamage = 45;
			if (Main.expertMode)
			{
				projType = ProjectileID.BoneGloveProj;
				proDamage = 80;
			}			
			if (Main.npc[NPCGlobal.Body].ai[0] >= 30 && Main.npc[NPCGlobal.Body].ai[0] <= 70)
			{
				if (npc.Distance(Main.npc[NPCGlobal.handright].Center) >= 80)
				{
					npc.velocity.X *= 0.98f;
					npc.velocity.Y *= 0.98f;
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
					{
						float rotation = (float)Math.Atan2((vector8.Y) - (Main.npc[NPCGlobal.handright].position.Y + (Main.npc[NPCGlobal.handright].height * 0.5f)), (vector8.X) - (Main.npc[NPCGlobal.handright].position.X - (Main.npc[NPCGlobal.handright].width * 0.5f)));
						npc.velocity.X = (float)(Math.Cos(rotation) * 16f) * -1;
						npc.velocity.Y = (float)(Math.Sin(rotation) * 16f) * -1;
					}
				}
				if (npc.Distance(Main.npc[NPCGlobal.handright].Center) <= 90)
				{
					if (!shoot)
					{
						Main.PlaySound(2, -1, -1, 12, 1f, 0f);
						int attackProj =  Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -6, -6, projType, proDamage, 0f, Main.myPlayer, 0f, npc.whoAmI);
						Main.projectile[attackProj].friendly = false;
						Main.projectile[attackProj].hostile = true;
						int attackProj2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -6, 0, projType, proDamage, 0f, Main.myPlayer, 0f, npc.whoAmI);
						Main.projectile[attackProj2].friendly = false;
						Main.projectile[attackProj2].hostile = true;
						int attackProj3 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -6, 6, projType, proDamage, 0f, Main.myPlayer, 0f, npc.whoAmI);
						Main.projectile[attackProj3].friendly = false;
						Main.projectile[attackProj3].hostile = true;
						shoot = true;
					}
					Vector2 vector90 = new Vector2(npc.Center.X, npc.Center.Y);
					float num757 = Main.npc[NPCGlobal.handright].Center.X - vector90.X;
					float num758 = Main.npc[NPCGlobal.handright].Center.Y - vector90.Y;
					num757 -= 15f;
					npc.velocity.X = num757;
					npc.velocity.Y = num758;
				}
			}
			if (Main.npc[NPCGlobal.Body].ai[0] >= 70 && Main.npc[NPCGlobal.Body].ai[0] <= 80)
			{
				shoot = false;
			}
		}
	}
}
