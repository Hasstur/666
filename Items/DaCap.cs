using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MORGEN.Items
{
	[AutoloadEquip(EquipType.Head)]

	public class DaCap : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.vanity = true;
			item.value = Item.buyPrice(0, 10, 50, 0);
			item.rare = ItemRarityID.Pink;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DaCap");
			Tooltip.SetDefault("LESS GOOOO");
		}
	}
}
