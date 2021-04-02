using Hgm.Engine.IO;
using Hgm.Engine.Utilities;

namespace Hgm.Engine.Assets
{
	public struct AssetLoadPass
	{
		public ResourceLocation ResourceName;
		public FileHandle File;
		
		public AssetLoadPass(ResourceLocation resourceLocation, FileHandle file)
		{
			ResourceName = resourceLocation;
			File = file;
		}
	}
}