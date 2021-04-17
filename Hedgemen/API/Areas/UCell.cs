using Hgm.Engine.GameState;
using Maths;

namespace Hgm.API.Areas
{
	public sealed class UCell
	{
		public MapPos Position { get; private set; }

		public GameProperties Properties { get; private set; } = new();

		public AreaNatureInfo NatureInfo { get; private set; } = new();

		public AreaCrawlerInfo CrawlerInfo { get; private set; } = new();
		
		public UCell(MapPos pos)
		{
			Position = pos;
		}
	}
}