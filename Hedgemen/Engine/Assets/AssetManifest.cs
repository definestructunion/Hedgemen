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
		
		//[JsonProperty("assets")]
		//private Dictionary<string, string> values = new Dictionary<string, string>();

		[JsonProperty("assets")]
		private List<AssetManifestEntry> entries = new();

		public AssetManifest()
		{
			
		}

		public List<ResourceName> AsResourceLocations()
		{
			return entries.Select(entry => new ResourceName(ns, entry.ResourceName)).ToList();
		}

		public List<FileHandle> AsFileHandles(DirectoryHandle modDirectory)
		{
			return entries.Select(entry => new FileHandle(modDirectory.FullName + "/" + entry.File)).ToList();
		}

		public List<AssetLoadPass> AsAssetLoadPasses(DirectoryHandle modDirectory)
		{
			return entries.Select(entry =>
			{
				var resourceLocation = new ResourceName(ns, entry.ResourceName);
				var file = new FileHandle(modDirectory.FullName + "/" + entry.File);
				return new AssetLoadPass(resourceLocation, file, entry.FileType);
			}).ToList();
		}
		
		public List<AssetLoadPass> AsAssetLoadPasses()
		{
			return entries.Select(entry =>
			{
				var resourceLocation = new ResourceName(ns, entry.ResourceName);
				var file = new FileHandle(entry.File);
				return new AssetLoadPass(resourceLocation, file, entry.FileType);
			}).ToList();
		}

		/*public List<ResourceName> AsResourceLocations()
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
		}*/
	}
}