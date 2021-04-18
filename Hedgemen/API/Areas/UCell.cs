using Hgm.Engine.GameState;
using Maths;

namespace Hgm.API.Areas
{
	public sealed class UCell
	{
		public MapPos Position { get; private set; }

		public GameProperties Properties { get; private set; } = new();

		public CellInfo CellInfo { get; set; } = new();

		public UCell(MapPos pos)
		{
			Position = pos;
		}
	}
}