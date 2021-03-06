﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hgm.Engine.IO;
using Hgm.Engine.Utilities;

namespace Hgm.API.Modding
{
	public sealed class HedgemenForge
	{
		public static string ResourceSuffixMod => "mod";
		
		public ForgeModPack ModPack { get; private set; } = null;

		private Dictionary<ResourceName, ForgeMod> mods = new Dictionary<ResourceName, ForgeMod>();
		
		public HedgemenForge()
		{
			
		}

		public void ForgeStart(HedgemenForgeArgs args)
		{
			ModPack = new ForgeModPack(args.ModPackDirectory);

			var modFolders = ModPack.ListMods();
			ForgeLoadDirectMods(args);
			ForgeLoadMods(args, modFolders);
			ForgeSetupMods();
		}

		private void ForgeSetupMods()
		{
			foreach(var mod in mods.Values)
			{
				mod.Setup();
			}
		}

		private void ForgeLoadDirectMods(HedgemenForgeArgs args)
		{
			foreach (var mod in args.DirectLoadMods)
			{
				Console.WriteLine("Loading: " + mod.Manifest.Name);
				mods.Add(new ResourceName(mod.Manifest.Namespace, ResourceSuffixMod), mod);
			}
		}

		private void ForgeLoadMods(HedgemenForgeArgs args, List<DirectoryHandle> modFolders)
		{
			foreach (var modFolder in modFolders)
			{
				var modManifest = ForgeGetManifestFromFolder(modFolder);

				Console.WriteLine("Loading: " + modManifest.Name);

				ForgeMod mod = null;
				
				var dllFile = modFolder.FindFile(modManifest.Depends.Dlls[0]);
				var modAssembly = Assembly.LoadFile(dllFile.FullName);

				for (int i = 0; i < modManifest.Depends.Dlls.Count; ++i)
				{
					var extraDllFile = modFolder.FindFile(modManifest.Depends.Dlls[i]);
					Assembly.LoadFile(extraDllFile.FullName);
				}

				var modMainType = modAssembly.GetType(modManifest.ModMain);

				if (modMainType == null) throw new Exception();
				
				mod = (ForgeMod) Activator.CreateInstance(modMainType, true);
				
				if (mod == null) throw new Exception();
				
				mod.Populate(modFolder, modManifest);
				mods.Add(new ResourceName(mod.Manifest.Namespace, ResourceSuffixMod), mod);
			}
		}
		
		private ForgeModManifest ForgeGetManifestFromFolder(DirectoryHandle modFolder)
		{
			var file = modFolder.FindFile("manifest.json");
			if (!file.Exists) return new ForgeModManifest();
			return file.ReadString().FromJson<ForgeModManifest>(JsonSettings.ManifestSettings());
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