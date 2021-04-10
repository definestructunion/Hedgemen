using Hgm.Engine.IO;

namespace Hgm.API.Modding
{
	public class ForgeMod
	{
		public DirectoryHandle Directory { get; private set; }
		
		public ForgeModManifest Manifest { get; private set; }

		public ForgeMod()
		{
			
		}

		public void Populate(
			DirectoryHandle directory, 
			ForgeModManifest manifest)
		{
			Directory = directory;
			Manifest = manifest;
		}

		public virtual void Setup()
		{
			
		}
	}
}