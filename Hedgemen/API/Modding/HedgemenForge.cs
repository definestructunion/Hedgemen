using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hgm.Engine.IO;
using Hgm.Engine.Utilities;
using ResourceLocation = Hgm.Engine.Utilities.ResourceLocation;

namespace Hgm.API.Modding
{
	public sealed class HedgemenForge
	{
		public static string ResourceSuffixMod => "mod";
		
		public ForgeModPack ModPack { get; private set; } = null;

		private Dictionary<ResourceLocation, ForgeMod> mods = new Dictionary<ResourceLocation, ForgeMod>();
		
		public HedgemenForge()
		{
			
		}

		public void ForgeStart(HedgemenForgeArgs args)
		{
			ModPack = new ForgeModPack(args.ModPackDirectory);

			var modFolders = ModPack.ListMods();
			ForgeLoadDirectMods(args);
			ForgeLoadMods(args, modFolders);
		}

		private void ForgeLoadDirectMods(HedgemenForgeArgs args)
		{
			foreach (var mod in args.DirectLoadMods)
			{
				Console.WriteLine("Loading: " + mod.Manifest.Name);
				mods.Add(new ResourceLocation(mod.Manifest.Namespace, ResourceSuffixMod), mod);
			}
		}

		private void ForgeLoadMods(HedgemenForgeArgs args, List<DirectoryHandle> modFolders)
		{
			foreach (var modFolder in modFolders)
			{
				var modManifest = ForgeGetManifestFromFolder(modFolder);
				var modLibraryManifest = ForgeGetLibraryManifestFromFolder(modFolder);
				var modDeregisterManifest = ForgeGetEditorialManifestFromFolder(modFolder);
			
				Console.WriteLine("Loading: " + modManifest.Name);

				ForgeMod mod = null;

				if (modManifest.ModMain == string.Empty)
				{
					mod = new ForgeMod();
				}

				else
				{
					var dllFile = modFolder.FindFile(modManifest.Depends.Dlls[0]);
					var modAssembly = Assembly.LoadFile(dllFile.FullName);
					
					mod = (ForgeMod)modAssembly.CreateInstance(modManifest.ModMain);
				}

				if (mod == null) throw new Exception();
				
				for (int i = 0; i < modManifest.Depends.Dlls.Count; ++i)
				{
					var extraDllFile = modFolder.FindFile(modManifest.Depends.Dlls[i]);
					Assembly.LoadFile(extraDllFile.FullName);
				}
				
				mod.Populate(modFolder, modManifest, modLibraryManifest, modDeregisterManifest);
				mods.Add(new ResourceLocation(mod.Manifest.Namespace, ResourceSuffixMod), mod);
			}
		}
		
		private ForgeModManifest ForgeGetManifestFromFolder(DirectoryHandle modFolder)
		{
			var file = modFolder.FindFile("manifest.json");
			if (!file.Exists) return new ForgeModManifest();
			return file.ReadString().FromJson<ForgeModManifest>(JsonSettings.ManifestSettings());
		}
		
		private ForgeLibraryManifest ForgeGetLibraryManifestFromFolder(DirectoryHandle modFolder)
		{
			var file = modFolder.FindFile("library_manifest.json");
			if (!file.Exists) return new ForgeLibraryManifest();
			return file.ReadString().FromJson<ForgeLibraryManifest>(JsonSettings.ManifestSettings());
		}
		
		private ForgeEditorialManifest ForgeGetEditorialManifestFromFolder(DirectoryHandle modFolder)
		{
			var file = modFolder.FindFile("editorial_manifest.json");
			if (!file.Exists) return new ForgeEditorialManifest();
			return file.ReadString().FromJson<ForgeEditorialManifest>(JsonSettings.ManifestSettings());
		}

		private bool ForgeShouldAddMod(HedgemenForgeArgs args, ForgeModManifest manifest, List<ForgeModManifest> modManifests)
		{
			foreach (var modManifest in modManifests)
			{
				if (modManifest.Depends.ModBlocks.Contains(manifest.Namespace)) return false;
			}

			return true;
		}

		private void ForgeAddMod(HedgemenForgeArgs args, ForgeMod mod)
		{
			
		}
	}

	public struct HedgemenForgeArgs
	{
		public DirectoryHandle ModPackDirectory;
		public List<ForgeMod> DirectLoadMods;
	}
}