using System;
using System.Globalization;
using System.Reflection;
using Hgm.Engine.IO;
using Microsoft.Xna.Framework.Media;

namespace Hgm.Engine.Audio
{
	public static class AssetCreator
	{
		public static Song CreateSong(FileHandle fileHandle)
		{
			object[] parameters = { fileHandle.FullName, string.Empty };
			BindingFlags songBindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
			CultureInfo songCultureInfo = CultureInfo.InvariantCulture;
			Song asset = Activator.CreateInstance(typeof(Song), songBindingFlags, null, parameters, songCultureInfo) as Song;
			return asset;
		}
	}
}