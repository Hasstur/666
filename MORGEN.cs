using Terraria;
using Terraria.ModLoader;

namespace MORGEN
{
	class MORGEN : Mod
	{
		public MORGEN()
		{
		}
		public override void Load()
		{
			if (!Main.dedServ)
			{
				AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Squaretron"), ItemType("SquaretronMusicBox"), TileType("SquaretronMusicBox"));
			}
		}
	}
}
