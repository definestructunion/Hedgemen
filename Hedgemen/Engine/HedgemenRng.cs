using System;

namespace Hgm.Engine
{
	public class HedgemenRng
	{
		public Random AreaRng { get; }
		public Random EntityRng { get; }
		
		public HedgemenRng()
		{
			AreaRng = new Random();
			EntityRng = new Random();
		}
	}
}