using Hgm.API.Areas;
using Hgm.API.Areas.World;
using Maths;

namespace Hgm.Content.Landscapers
{
	public sealed class LandscaperOverworld : Landscaper
	{
		public override void Generate(UArea area)
		{
			for (int y = 0; y < area.AreaMap.Height; ++y)
			{
				for (int x = 0; x < area.AreaMap.Width; ++x)
				{
					var pos = new MapPos(x, y);
					var cell = new UCell(pos);
					area.AreaMap.SetCellAt(pos, cell);
				}
			}
		}

		public override bool ShouldGenerate(UArea area)
		{
			return area.TypeInfo.ResourceName.Equals(HedgemenMod.AreaOverworldName);
		}
	}
}