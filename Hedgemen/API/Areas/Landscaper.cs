namespace Hgm.API.Areas
{
	public abstract class Landscaper
	{
		public abstract void Generate(UArea area);
		public abstract bool ShouldGenerate(UArea area);
	}
}