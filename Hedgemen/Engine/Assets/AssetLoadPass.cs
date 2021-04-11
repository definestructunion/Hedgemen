using Hgm.Engine.IO;
using Hgm.Engine.Utilities;

namespace Hgm.Engine.Assets
{
	public struct AssetLoadPass
	{
		public ResourceName ResourceName;
		public FileHandle File;
		public AssetLoadType AssetType;
		
		public AssetLoadPass(ResourceName resourceName, FileHandle file, AssetLoadType assetType = AssetLoadType.Default)
		{
			AssetType = assetType;
			ResourceName = resourceName;
			File = file;
		}
	}
}