using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MORGEN
{
    public class MPlayer : ModPlayer
    {
        public override bool Autoload(ref string name) { return true; }

        public bool stonedStone;
		public bool alphaCard;
		public bool cashback;

        public override void ResetEffects()
        {
			stonedStone = false;
			alphaCard = false;
		}
		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
			if (alphaCard && crit)
				cashback = true;
		}
		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (alphaCard && crit)
				cashback = true;
		}
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{			
			if (stonedStone && Main.rand.Next(5) == 0)
			{
				if (damage >= player.statLife)
				{
					Main.PlaySound(SoundID.Drip, (int)player.position.X, (int)player.position.Y);
					player.statLife += player.statLifeMax2/2;
					player.AddBuff(BuffID.Stoned, 60 * 5, true);
				}
			}
			return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
		}
	}
}