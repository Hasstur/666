using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace MORGEN.Items
{
    class EliteSnus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elite Snus");
            Tooltip.SetDefault("Increases critical strike chance by 5%");
            DisplayName.AddTranslation(GameCulture.Russian, "Элитный снюсик");
            Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает критический урон на 5%");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 14;
            item.accessory = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Pink;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeCrit += 5;
            player.magicCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;
        }
    }
}
﻿