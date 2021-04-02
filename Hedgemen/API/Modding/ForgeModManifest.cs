using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hgm.API.Modding
{
	public sealed class ForgeModManifest
	{
		[JsonProperty("schema_version")]
		private int schemaVersion = 1;

		public int SchemaVersion => schemaVersion;

		[JsonProperty("namespace")]
		private string nameSpace = "unnamed";

		public string Namespace => nameSpace;

		[JsonProperty("version")]
		private string version = "0.0.1";

		public string Version => version;

		[JsonProperty("name")]
		private string name = "Unnamed Mod";

		public string Name => name;

		[JsonProperty("description")]
		private string description = "I haven't made a a description yet!";

		public string Description => description;
		
		[JsonProperty("authors")]
		private List<string> authors = new List<string>();

		public IReadOnlyList<string> Authors => authors;
		
		[JsonProperty("contact")]
		private ForgeModManifestContactInfo contact = new ForgeModManifestContactInfo();

		public ForgeModManifestContactInfo Contact => contact;
		
		[JsonProperty("depends")]
		private ForgeModManifestDependenciesInfo depends = new ForgeModManifestDependenciesInfo();

		public ForgeModManifestDependenciesInfo Depends => depends;

		[JsonProperty("mod_main")]
		private string modMain = string.Empty;

		[JsonProperty]
		private bool isOverhaul = false;

		public bool IsOverhaul => isOverhaul;

		public string ModMain => modMain;
	}
}