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
			for(int y = 0; y < Height; ++y)
			{
				for(int x = 0; x < Width; ++x)
				{
					var pos = new MapPos(x, y);
					SetCellAt(pos, new UCell(pos));
				}
			}
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