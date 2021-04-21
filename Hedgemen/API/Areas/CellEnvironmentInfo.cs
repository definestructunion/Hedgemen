using Hgm.API.Areas.World;

namespace Hgm.API.Areas
{
    public class CellEnvironmentInfo
    {
        public Terrain Terrain { get; set; } = new TerrainEmpty();
        public TerrainFeature TerrainFeature { get; set; } = new TerrainFeatureEmpty();
        public Biome Biome { get; set; } = new BiomeEmpty();
        public float HeightValue { get; set; } = 0.0f;

        public CellEnvironmentInfo()
        {

        }
    }
}