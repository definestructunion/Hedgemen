namespace Hgm.API.Areas
{
	public class LandscaperDefault : Landscaper
	{
		public override void Generate(UArea area)
		{
			
		}

		public override bool ShouldGenerate(UArea area)
		{
			return false;
		}
	}
}