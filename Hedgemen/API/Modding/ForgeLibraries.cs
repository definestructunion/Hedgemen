using System;
using Hgm.API.Areas;

namespace Hgm.API.Modding
{
	public sealed class ForgeLibraries
	{
		public ForgeLibrary<UAreaTypeInfo> AreaTypes { get; private set; } = new("areas");
		public ForgeLibrary<Func<IAreaBehaviour>> AreaBehaviours { get; private set; } = new("area_behaviours");

		public ForgeLibraries()
		{
			
		}
	}
}