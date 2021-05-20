using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace MORGEN.Items
{
    class PlatinumDiscountCard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Platinum Discount Card");
            Tooltip.SetDefault("Enemies drop money on every crit");
            DisplayName.AddTranslation(GameCulture.Russian, "Альфа Карта");
            Tooltip.AddTranslation(GameCulture.Russian, "Из врагов выпадают деньги при получени критического урона");
        }
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 28;
            item.accessory = true;
            item.value = Item.buyPrice(0, 0, 0, 0);
            item.rare = ItemRarityID.Pink;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MPlayer>().alphaCard = true;
        }
    }
}
﻿