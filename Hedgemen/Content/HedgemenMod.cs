using Hgm.API.Entities;
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
			Populate(directory, manifest);
		}

		public override void Setup()
		{
			Hedgemen.Libraries.EntityTypes.Register(CreateEntityTypeHuman());
		}

		private UEntityTypeInfo CreateEntityTypeHuman()
		{
			return new UEntityTypeInfo
			{
				ResourceName = "hedgemen:entities/human",
				DefaultNames = {"Reynauld", "Dismas", "Xerxes"}
			};
		}
	}
}