using System;
using System.IO;
using System.IO.Compression;

namespace Hgm.Engine.IO
{
	public class ZipFileHandle : FileHandle
	{
		public FileHandle Zip { get; protected set; }
		public string InternalAssetPath { get; protected set; }

		public ZipFileHandle(FileHandle zipFile, string internalAssetPath)
		: base(internalAssetPath, FsType.Internal)
		{
			Zip = zipFile;
			this.InternalAssetPath = internalAssetPath;
		}

		public bool InternalAssetPathExists => CreateZipArchive()?.GetEntry(InternalAssetPath) != null;

		public override Stream Open(FileMode mode = FileMode.Open)
		{
			var zip = CreateZipArchive(mode);
			var asset = zip.GetEntry(InternalAssetPath);
			
			if(asset == null) throw new IOException("Can't open entry \"" + InternalAssetPath + "\"");

			return asset.Open();
		}
		
		private ZipArchive CreateZipArchive(FileMode mode = FileMode.Open)
		{
			try
			{
				var zip = new ZipArchive(new FileStream(Zip.FullName, mode), ZipArchiveMode.Update, false);
				return zip;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
	}
}