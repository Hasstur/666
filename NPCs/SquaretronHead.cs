using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace MORGEN.NPCs
{
    [AutoloadBossHead]
    public class SquaretronHead : ModNPC
    {
        private int musicType;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squaretron");
        }
        public override void SetDefaults()
        {
            npc.width = 80;
            npc.height = 110;
            npc.aiStyle = -1;
            Main.npcFrameCount[npc.type] = 1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.damage = 66;
            npc.defense = 16;
            npc.lifeMax = 16660;
            npc.dontTakeDamage = false;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath18;
            npc.knockBackResist = 0f;
            npc.boss = true;
            bossBag = mod.ItemType("SquaretronBag");
            musicType = Main.rand.Next(1, 666);
            if (musicType == 666)
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Squaretron666");
            else
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Squaretron");
        }
        public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
        {
            npc.damage = (int)(npc.damage * 0.65f);
            npc.defense = 36;
            npc.lifeMax = 16660;
        }
        public override void AI()
        {
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            if (!NPC.AnyNPCs(mod.NPCType("SquaretronBody")))
            {
                npc.active = false;
            }

            NPCGlobal.Boss = npc.whoAmI;

            int projType = ProjectileID.CopperCoin;
            int proDamage = 50;
            if (npc.life <= npc.lifeMax * 0.8f)
            {
                projType = ProjectileID.SilverCoin;
                proDamage = 65;
            }
            if (npc.life <= npc.lifeMax * 0.5f)
            {
                projType = ProjectileID.GoldCoin;
                proDamage = 80;
            }
            if (npc.life <= npc.lifeMax * 0.25f)
            {
                projType = ProjectileID.PlatinumCoin;
                proDamage = 80;
            }
            if (Main.expertMode)
            {
                proDamage *= 3 / 2;
                projType = ProjectileID.PlatinumCoin;
            }

            if (Main.npc[NPCGlobal.Body].ai[0] >= 100 && Main.npc[NPCGlobal.Body].ai[0] <= 160 && Main.npc[NPCGlobal.Body].ai[0] % 12 == 0)
            {
                Main.PlaySound((int)SoundType.Item, npc.Center, 95);
                Vector2 velocity = Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * 15;
                int attackProj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 20, velocity.X, velocity.Y, projType, proDamage, 0f, Main.myPlayer, 0f, npc.whoAmI);
                Main.projectile[attackProj].friendly = false;
                Main.projectile[attackProj].hostile = true;
            }

            Vector2 vector90 = new Vector2(npc.Center.X, npc.Center.Y);
            float num757 = Main.npc[NPCGlobal.Body].Center.X - vector90.X;
            float num758 = Main.npc[NPCGlobal.Body].Center.Y - vector90.Y;
            num758 -= 100f;
            npc.velocity.X = num757;
            npc.velocity.Y = num758;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Main.npc[NPCGlobal.Body].active = false;
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Gore1"), 1f);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y - 10), npc.velocity, mod.GetGoreSlot("Gores/Gore2"), 1f);
                Gore.NewGore(new Vector2(npc.position.X - 300, npc.position.Y), npc.velocity, mod.GetGoreSlot("NPCs/SquaretronHand"), 1f);
                Gore.NewGore(new Vector2(npc.position.X + 300, npc.position.Y), npc.velocity, mod.GetGoreSlot("NPCs/SquaretronHand"), 1f);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y - 50), npc.velocity, mod.GetGoreSlot("NPCs/SquaretronBody"), 1f);
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void NPCLoot()
        {
            if (!MWorld.downedMorgen)
            {
                MWorld.downedMorgen = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
            if (Main.expertMode)
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y + Main.rand.Next(-5, 5), 0, 0, ProjectileID.CoinPortal, 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                npc.DropBossBags();
                return;
            }
            int drop = Main.rand.Next(4);
            if (drop == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldRing);
            }
            if (drop == 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PlatinumDiscountCard"));
            }
            if (drop == 2)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EliteSnus"));
            }
            if (drop == 3)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.CoinGun);
            }

            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DaCap"), 1);
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SquaretronTrophy"), 1);
            }

            Projectile.NewProjectile(npc.Center.X, npc.Center.Y + Main.rand.Next(-5, 5), 0, 0, ProjectileID.CoinPortal, 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
        }
    }
}
