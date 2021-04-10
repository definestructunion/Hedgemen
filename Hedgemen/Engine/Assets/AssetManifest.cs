using System;
using System.Collections.Generic;
using System.Linq;
using Hgm.Engine.IO;
using Hgm.Engine.Utilities;
using Newtonsoft.Json;

namespace Hgm.Engine.Assets
{
	public class AssetManifest
	{
		[JsonProperty("namespace")]
		private string ns = ResourceName.EmptyNamespace;
		
		public string Namespace => ns;
		
		[JsonProperty("assets")]
		private Dictionary<string, string> values = new Dictionary<string, string>();

		public AssetManifest()
		{
			
		}

		public List<ResourceName> AsResourceLocations()
		{
			return values.Select(keyValuePair => new ResourceName(ns, keyValuePair.Key)).ToList();
		}

		public List<FileHandle> AsFileHandles(DirectoryHandle modDirectory)
		{
			return values.Select(keyValuePair => new FileHandle(modDirectory.FullName + "/" + keyValuePair.Value)).ToList();
		}

		public List<AssetLoadPass> AsAssetLoadPasses(DirectoryHandle modDirectory)
		{
			return values.Select(keyValuePair =>
			{
				var resourceLocation = new ResourceName(ns, keyValuePair.Key);
				var file = new FileHandle(modDirectory.FullName + "/" + keyValuePair.Value);
				return new AssetLoadPass(resourceLocation, file);
			}).ToList();
		}
		
		public List<AssetLoadPass> AsAssetLoadPasses()
		{
			return values.Select(keyValuePair =>
			{
				var resourceLocation = new ResourceName(ns, keyValuePair.Key);
				var file = new FileHandle(keyValuePair.Value);
				return new AssetLoadPass(resourceLocation, file);
			}).ToList();
		}
	}
}