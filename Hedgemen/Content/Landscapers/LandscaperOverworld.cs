using Hgm.API.Areas;
using Hgm.API.Areas.Dungeon;
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
					var cell = area.AreaMap.GetCellAt(pos);
					cell.EnvironmentInfo.Biome = new BiomeGenericDungeon();
					cell.EnvironmentInfo.HeightValue = x * y;
				}
			}
		}

		public override bool ShouldGenerate(UArea area)
		{
			return area.TypeInfo.ResourceName.Equals(HedgemenMod.AreaOverworldName);
		}
	}
}