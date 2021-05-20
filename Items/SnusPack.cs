using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace MORGEN.Items
{
    public class SnusPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suspicious Snus Pack");
            Tooltip.SetDefault("666% strength\nSummons Squaretron");
            DisplayName.AddTranslation(GameCulture.Russian, "Подозрительная пачка снюса");
            Tooltip.AddTranslation(GameCulture.Russian, "666% крепости\nПризывает Квадратрона");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;
            item.maxStack = 20;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = ItemRarityID.LightRed;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("SquaretronBody"));
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("SquaretronBody"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}