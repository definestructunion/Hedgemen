using System;
using Hgm.API.Areas;
using Hgm.API.Entities;
using Hgm.API.Modding;
using Hgm.Content.Landscapers;
using Hgm.Engine.IO;
using Hgm.Engine.Utilities;

namespace Hgm.Content
{
	public sealed class HedgemenMod : ForgeMod
	{
		public static ResourceName AreaOverworldName => "hedgemen:areas/overworld";
		public static ResourceName AreaOverworldCartographerName => "hedgemen:areas/cartographers/overworld";
		public static ResourceName AreaOverworldLandscaperName => "hedgemen:areas/landscapers/overworld";

		public static UAreaTypeInfo AreaOverworld => Hedgemen.Libraries.AreaTypes[AreaOverworldName];
		public static UCartographer AreaOverworldCartographer => Hedgemen.Libraries.Cartographers[AreaOverworldCartographerName];
		public static Func<Landscaper> AreaOverworldLandscaper => Hedgemen.Libraries.Landscapers[AreaOverworldLandscaperName];
		
		public HedgemenMod()
		{
			var directory = new DirectoryHandle("hedgemen");
			var manifest = new FileHandle("hedgemen/manifest.json").ReadString().FromJson<ForgeModManifest>(JsonSettings.ManifestSettings());
			Populate(directory, manifest);
		}

		public override void Setup()
		{
			Console.WriteLine("Setting up Hedgemen!");
			Hedgemen.Libraries.AreaTypes.Register(CreateAreaTypeOverworld());
			Hedgemen.Libraries.EntityTypes.Register(CreateEntityHumanArcher());
			
			Hedgemen.Libraries.Cartographers.Register(CreateCartographerOverworld());
			Hedgemen.Libraries.Landscapers.Register(AreaOverworldLandscaperName, () => new LandscaperOverworld());
		}

		private UAreaTypeInfo CreateAreaTypeOverworld()
		{
			return new UAreaTypeInfo
			{
				ResourceName = AreaOverworldName,
				AreaBehaviourName = ResourceName.Empty,
				AreaCartographerName = AreaOverworldCartographerName,
				Width = 512,
				Height = 512,
				Name = "Overworld"
			};
		}

		private UEntityTypeInfo CreateEntityHumanArcher()
		{
			return new UEntityTypeInfo
			{
				ResourceName = "hedgemen:entities/human_archer",
				EntityBehaviourName = ResourceName.Empty,
				Names = {"Reynauld", "Dismas", "Xerxes"}
			};
		}

		private UCartographer CreateCartographerOverworld()
		{
			return new UCartographer
			{
				ResourceName = AreaOverworldCartographerName,
				LandscaperNames = { AreaOverworldLandscaperName }
			};
		}
	}
}