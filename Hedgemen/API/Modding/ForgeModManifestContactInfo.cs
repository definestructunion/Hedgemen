using Newtonsoft.Json;

namespace Hgm.API.Modding
{
	public class ForgeModManifestContactInfo
	{
		public ForgeModManifestContactInfo()
		{
			
		}
		
		[JsonProperty("homepage")]
		private string homepage = "https://hedgemen.com";

		public string Homepage => homepage;
		
		[JsonProperty("source")]
		private string source = "https://hedgemen.com";

		public string Source => source;
	}
}