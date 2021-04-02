using Hgm.Engine.IO;

namespace Hgm.API.Modding
{
	public class ForgeMod
	{
		public DirectoryHandle Directory { get; private set; }
		
		public ForgeModManifest Manifest { get; private set; }
		
		public ForgeLibraryManifest LibraryManifest { get; private set; }
		
		public ForgeEditorialManifest EditorialManifest { get; private set; }
		
		public ForgeMod()
		{
			
		}

		public void Populate(
			DirectoryHandle directory, 
			ForgeModManifest manifest,
			ForgeLibraryManifest libraryManifest,
			ForgeEditorialManifest editorialManifest)
		{
			Directory = directory;
			Manifest = manifest;
			LibraryManifest = libraryManifest;
			EditorialManifest = editorialManifest;
		}
	}
}