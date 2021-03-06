﻿namespace Maths
{
	public struct MapPos
	{
		public int X { get; set; }
		public int Y { get; set; }

		public MapPos(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return "<x:" + X + ",y:" + Y + ">";
		}
	}
}