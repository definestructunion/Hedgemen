using Newtonsoft.Json;

namespace Hgm.Engine.Assets
{
    public class AssetManifestEntry
    {
        [JsonProperty("resource_name")]
        private string resourceName;

        public string ResourceName => resourceName;

        [JsonProperty("file")]
        private string file;

        public string File => file;

        [JsonProperty("file_type")]
        private AssetLoadType fileType;
        
        public AssetLoadType FileType => fileType;
    }
}