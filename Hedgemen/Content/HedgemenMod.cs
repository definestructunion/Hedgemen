using Hgm.API.Modding;
using Hgm.Engine.IO;
using Hgm.Engine.Utilities;

namespace Hgm.Content
{
	public sealed class HedgemenMod : ForgeMod
	{
		public HedgemenMod()
		{
			var directory = new DirectoryHandle("hedgemen");
			var manifest = new FileHandle("hedgemen/manifest.json").ReadString().FromJson<ForgeModManifest>(JsonSettings.ManifestSettings());
			var libraryManifest = new FileHandle("hedgemen/library_manifest.json").ReadString().FromJson<ForgeLibraryManifest>(JsonSettings.ManifestSettings());
			var editorialManifest = new FileHandle("hedgemen/editorial_manifest.json").ReadString().FromJson<ForgeEditorialManifest>(JsonSettings.ManifestSettings());
			Populate(directory, manifest, libraryManifest, editorialManifest);
		}
	}
}