using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MORGEN
{
	public class MWorld : ModWorld
	{
		public static bool downedMorgen;

		public override void Initialize()
		{
			downedMorgen = false;
		}

		public override TagCompound Save()
		{
			var downed = new List<string>();
			if (downedMorgen)
			{
				downed.Add("Squaretron");
			}
			return new TagCompound
			{
				{"downed", downed}
			};
		}

		public override void Load(TagCompound tag)
		{
			var downed = tag.GetList<string>("downed");
			downedMorgen = downed.Contains("Squaretron");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte flags = reader.ReadByte();
				downedMorgen = flags[0];
			}
			else
			{
				mod.Logger.WarnFormat("Legendary Mod: Unknown loadVersion: {0}", loadVersion);
			}
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = downedMorgen;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedMorgen = flags[0];
		}
	}
}
