using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hgm.API.Modding
{
	public class ForgeModManifestDependenciesInfo
	{
		public ForgeModManifestDependenciesInfo()
		{
			
		}
		
		[JsonProperty("mods")]
		private List<string> mods = new List<string>();

		public IReadOnlyList<string> Mods => mods;

		[JsonProperty("incompatible_mods")]
		private List<string> incompatibleMods = new List<string>();

		public IReadOnlyList<string> ModBlocks => incompatibleMods.AsReadOnly();
		
		[JsonProperty("dlls")] 
		private List<string> dlls = new List<string>();

		public IReadOnlyList<string> Dlls => dlls.AsReadOnly();
		
		[JsonProperty("hedgemen")]
		private string hedgemen = "0.0.1";

		public string Hedgemen => hedgemen;
	}
}