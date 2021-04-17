using System.Collections.Generic;
using Hgm.API.Resources;
using Hgm.Engine.Utilities;

namespace Hgm.API.Areas
{
	public sealed class UCartographer : UTypeInfo<UCartographer>
	{
		public List<ResourceName> LandscaperNames = new ();
		
		public void Generate(UArea area)
		{
			var landscapers = new List<Landscaper>(LandscaperNames.Count);

			foreach (var landscaperName in LandscaperNames)
			{
				landscapers.Add(Hedgemen.Libraries.Landscapers[landscaperName]());
			}

			foreach (var landscaper in landscapers)
			{
				if (!landscaper.ShouldGenerate(area)) continue;
				landscaper.Generate(area);
			}
		}
	}
}