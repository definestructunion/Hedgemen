using Maths;

namespace Hgm.API.Areas
{
	public class UAreaMap
	{
		private UCell[,] cells;

		public UAreaMap(int width, int height)
		{
			cells = new UCell[width, height];
		}

		public UCell CellAt(MapPos pos)
		{
			return cells[pos.X, pos.Y];
		}
	}
}