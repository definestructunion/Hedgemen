using Maths;

namespace Hgm.API.Areas
{
	public sealed class UAreaMap
	{
		private UCell[,] cells;

		public int Width => cells.GetLength(0);

		public int Height => cells.GetLength(1);
		
		public UAreaMap(int width, int height)
		{
			cells = new UCell[width, height];
		}

		public UCell GetCellAt(MapPos pos)
		{
			return cells[pos.X, pos.Y];
		}

		public UCell SetCellAt(MapPos pos, UCell cell)
		{
			cells[pos.X, pos.Y] = cell;
			return cell;
		}
	}
}