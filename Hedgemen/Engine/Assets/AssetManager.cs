using System;
using System.Collections.Generic;
using System.IO;
using Hgm.Engine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Hgm.Engine.Assets
{
	public class AssetManager : ContentManager
	{
		private Dictionary<ResourceLocation, object> assets;

		private Dictionary<ResourceLocation, AssetLoadPass> loadPasses;
		
		public GraphicsDeviceManager Graphics { get; private set; }
		
		public AssetManager(Game game, GraphicsDeviceManager graphics) : base(game.Services, "")
		{
			Graphics = graphics;
			assets = new Dictionary<ResourceLocation, object>();
			loadPasses = new Dictionary<ResourceLocation, AssetLoadPass>();
		}

		public T Load<T>(ResourceLocation resource)
		{
			return (T)assets[resource];
		}

		public T LoadDirect<T>(ResourceLocation resourceName, T asset)
		{
			if (assets.ContainsKey(resourceName))
				return (T) assets[resourceName];
			
			assets.Add(resourceName, asset);

			return asset;
		}

		public T Load<T>(AssetLoadPass loadPass)
		{
			if (assets.ContainsKey(loadPass.ResourceName))
				return (T)assets[loadPass.ResourceName];
			
			// hack to avoid ContentLoadException with SongReader.Read with slashes
			ResourceLocation indexNameSlashesReplaced = loadPass.ResourceName.FullName.Replace("/", "___").Replace("\\", "___");
			loadPasses.Add(indexNameSlashesReplaced, loadPass);
			
			var asset = ReadAsset<T>(indexNameSlashesReplaced, null);

			assets.Add(loadPass.ResourceName, asset);
			
			return asset;
		}

		protected override Stream OpenStream(string assetName)
		{
			var resource = new ResourceLocation(assetName);

			var loadPass = loadPasses[resource];
			
			return loadPass.File.Open();
		}
	}
}