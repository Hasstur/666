using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace MORGEN.NPCs
{
	public class SquaretronHand2 : ModNPC
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
			Vector2 target = Main.npc[NPCGlobal.Boss].Bottom + new Vector2(320f, 35f);
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
			NPCGlobal.handright = npc.whoAmI;
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
				if (npc.Distance(Main.npc[NPCGlobal.handleft].Center) >= 80)
				{
					npc.velocity.X *= 0.98f;
					npc.velocity.Y *= 0.98f;
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
					{
						float rotation = (float)Math.Atan2((vector8.Y) - (Main.npc[NPCGlobal.handleft].position.Y + (Main.npc[NPCGlobal.handleft].height * 0.5f)), (vector8.X) - (Main.npc[NPCGlobal.handleft].position.X - (Main.npc[NPCGlobal.handleft].width * 0.5f)));
						npc.velocity.X = (float)(Math.Cos(rotation) * 16f) * -1;
						npc.velocity.Y = (float)(Math.Sin(rotation) * 16f) * -1;
					}
				}
				if (npc.Distance(Main.npc[NPCGlobal.handleft].Center) <= 90)
				{
					Main.npc[NPCGlobal.Body].velocity = Vector2.Zero;
					if (!shoot)
					{
						int num23 = 36;
						for (int index1 = 0; index1 < num23; ++index1)
						{
							Vector2 vector2_3 = (Vector2.Normalize(npc.velocity) * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f).RotatedBy((double)(index1 - (num23 / 2 - 1)) * 6.28318548202515 / (double)num23, new Vector2()) + npc.Center;
							Vector2 vector2_4 = vector2_3 - npc.Center;
							int index2 = Dust.NewDust(vector2_3 + vector2_4, 0, 0, DustID.GoldCoin, vector2_4.X * 2f, vector2_4.Y * 2f, 100, new Color(), 1.4f);
							Main.dust[index2].noGravity = true;
							Main.dust[index2].noLight = true;
							Main.dust[index2].velocity = Vector2.Normalize(vector2_4) * 3f;
						}
						Main.PlaySound(2, -1, -1, 12, 1f, 0f);
						int attackProj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 6, -6, projType, proDamage, 0f, Main.myPlayer, 0f, npc.whoAmI);
						Main.projectile[attackProj].friendly = false;
						Main.projectile[attackProj].hostile = true;
						int attackProj2 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 6, 0, projType, proDamage, 0f, Main.myPlayer, 0f, npc.whoAmI);
						Main.projectile[attackProj2].friendly = false;
						Main.projectile[attackProj2].hostile = true;
						int attackProj3 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 6, 6, projType, proDamage, 0f, Main.myPlayer, 0f, npc.whoAmI);
						Main.projectile[attackProj3].friendly = false;
						Main.projectile[attackProj3].hostile = true;
						shoot = true;
					}
					Vector2 vector90 = new Vector2(npc.Center.X, npc.Center.Y);
					float num757 = Main.npc[NPCGlobal.Body].Center.X - vector90.X;
					float num758 = Main.npc[NPCGlobal.Body].Center.Y - vector90.Y;
					num758 += 64f;
					num757 += 15f;
					npc.velocity.X = num757;
					npc.velocity.Y = num758;
				}
			}
			if (Main.npc[NPCGlobal.Body].ai[0] >= 70 && Main.npc[NPCGlobal.Body].ai[0] <= 80)
			{
				shoot = false;
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("NPCs/SquaretronHand"), 1f);
		}
	}
}
