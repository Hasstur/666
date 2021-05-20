using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MORGEN.Items
{
    public class SquaretronBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.LightRed;
            item.expert = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right click to open");
        }
        public override int BossBagNPC => ModContent.NPCType<NPCs.SquaretronHead>();
        public override bool CanRightClick()
        {
            return true;
        }
        public override void OpenBossBag(Player player)
        {
            int drop = Main.rand.Next(4);
            if (drop == 0)
            {
                player.QuickSpawnItem(mod.ItemType("ItemID.GoldRing"));
            }
            if (drop == 1)
            {
                player.QuickSpawnItem(mod.ItemType("PlatinumDiscountCard"));
            }
            if (drop == 2)
            {
                player.QuickSpawnItem(mod.ItemType("EliteSnus"));
            }
            if (drop == 3)
            {
                player.QuickSpawnItem(ItemID.CoinGun);
            }

            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("DaCap"));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("SquaretronTrophy"));
            }         
            player.QuickSpawnItem(mod.ItemType("IcedChain"));
        }
    }
}
