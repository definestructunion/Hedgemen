using Newtonsoft.Json;

namespace Hgm.API.Modding
{
	public sealed class ForgeLibraryManifestEntry
	{
		[JsonProperty("resource_name")]
		public string ResourceName = "null";
		
		[JsonProperty("library_name")]
		public string LibraryName = "other";
	}
}