namespace Hgm.API.Areas
{
    public class CellSector
    {
        private CellRegion[] regions = new CellRegion[4];

        public CellSector()
        {
            InitializeRegions();
        }

        private void InitializeRegions()
        {
            for(int i = 0; i < regions.Length; ++i)
                regions[i] = new CellRegion();
        }

        public CellRegion GetRegion(int index)
        {
            if(index < 0  || index >= regions.Length) return null;
            return regions[index];
        }

        public void SetRegion(int index, CellRegion region)
        {
            if(index < 0  || index >= regions.Length) return;
            regions[index] = region;
        }
    }
}