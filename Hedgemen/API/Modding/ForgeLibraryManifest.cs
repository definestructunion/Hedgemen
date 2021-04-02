using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hgm.API.Modding
{
	public sealed class ForgeLibraryManifest
	{
		[JsonProperty("entries")]
		private List<ForgeLibraryManifestEntry> entries = new();

		public IReadOnlyList<ForgeLibraryManifestEntry> Entries => entries.AsReadOnly();
		
		
	}
}