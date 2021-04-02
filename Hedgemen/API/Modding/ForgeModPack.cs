using System.Collections.Generic;
using Hgm.Engine.IO;

namespace Hgm.API.Modding
{
	public sealed class ForgeModPack
	{
		public DirectoryHandle Folder { get; private set; }
		
		public ForgeModPack(DirectoryHandle folder)
		{
			Folder = folder;
		}

		public List<DirectoryHandle> ListMods()
		{
			return Folder.ListDirectories(e => e.FindFile("manifest.json").Exists);
		}
	}
}