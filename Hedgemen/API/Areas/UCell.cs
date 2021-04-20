using Hgm.Engine.GameState;
using Maths;

namespace Hgm.API.Areas
{
	public sealed class UCell
	{
		public MapPos Position { get; private set; }

		public CellEnvironmentInfo EnvironmentInfo { get; set; } = new CellEnvironmentInfo();

		public CellSector Sector { get; set; } = new();

		public UCell(MapPos pos)
		{
			Position = pos;
		}
	}
}