using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace MORGEN.Items
{
    [AutoloadEquip(EquipType.Neck)]
    class IcedChain : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iced Out Chain");
            Tooltip.SetDefault("'Real ice'" + "\nHas a chance to prevent death and turn player into stone for 5 seconds");
            DisplayName.AddTranslation(GameCulture.Russian, "Дорогая Цепь");
            Tooltip.AddTranslation(GameCulture.Russian, "'Закаменел, будто каменный камень'" + "\nМожет предотвратить смерть и превратить в камень на 5 секунд");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.accessory = true;
            item.value = Item.buyPrice(3, 0, 0, 0);
            item.rare = ItemRarityID.Pink;
            item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MPlayer>().stonedStone = true;
        }
    }
}
﻿