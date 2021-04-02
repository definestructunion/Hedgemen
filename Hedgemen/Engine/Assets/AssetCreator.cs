using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Hgm.Engine.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using ResourceLocation = Hgm.Engine.Utilities.ResourceLocation;

namespace Hgm.Engine.Assets
{
	public static class AssetCreator
	{
		public static Song CreateSong(AssetManager assets, ResourceLocation resourceName, FileHandle fileHandle)
		{
			object[] parameters = { fileHandle.FullName, string.Empty };
			BindingFlags songBindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
			CultureInfo songCultureInfo = CultureInfo.InvariantCulture;
			Song asset = Activator.CreateInstance(typeof(Song), songBindingFlags, null, parameters, songCultureInfo) as Song;
			assets.LoadDirect(resourceName, asset);
			return asset;
		}

		public static Texture2D CreateTexture2D(AssetManager assets, ResourceLocation resourceName, FileHandle fileHandle)
		{
			var asset = Texture2D.FromStream(assets.Graphics.GraphicsDevice, fileHandle.Open(FileMode.Open));
			assets.LoadDirect(resourceName, asset);
			return asset;
		}

		public static SoundEffect CreateSoundEffect(AssetManager assets, ResourceLocation resourceName, FileHandle fileHandle)
		{
			var asset = SoundEffect.FromStream(fileHandle.Open(FileMode.Open));
			assets.LoadDirect(resourceName, asset);
			return asset;
		}

		public static Effect CreateEffect(AssetManager assets, ResourceLocation resourceName, FileHandle fileHandle)
		{
			return assets.Load<Effect>(new AssetLoadPass
			{
				ResourceName = resourceName,
				File = fileHandle
			});
		}

		public static SpriteFont CreateSpriteFont(AssetManager assets, ResourceLocation resourceName, FileHandle fileHandle)
		{
			return assets.Load<SpriteFont>(new AssetLoadPass
			{
				ResourceName = resourceName,
				File = fileHandle
			});
		}
	}
}