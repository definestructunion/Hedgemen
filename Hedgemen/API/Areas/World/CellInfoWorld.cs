namespace Hgm.API.Areas.World
{
    public class CellInfoWorld : CellInfo
    {
        public Terrain Terrain { get; set; }
        public TerrainFeature TerrainFeature { get; set; }
        public Biome Biome { get; set; }
    }
}