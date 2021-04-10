using Hgm.Engine.IO;
using Hgm.Engine.Utilities;

namespace Hgm.Engine.Assets
{
	public struct AssetLoadPass
	{
		public ResourceName ResourceName;
		public FileHandle File;
		
		public AssetLoadPass(ResourceName resourceName, FileHandle file)
		{
			ResourceName = resourceName;
			File = file;
		}
	}
}