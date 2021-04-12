using System;
using Hgm.API.Areas;
using Hgm.API.Entities;
using Hgm.Engine.Utilities;

namespace Hgm.API.Modding
{
	public sealed class ForgeLibraries
	{
		public ForgeLibrary<UAreaTypeInfo> AreaTypes { get; private set; } = new("areas");
		public ForgeLibrary<ResourceName, Func<IAreaBehaviour>> AreaBehaviours { get; private set; } = new("area_behaviours");

		public ForgeLibrary<UEntityTypeInfo> EntityTypes { get; private set; } = new("entities");

		public ForgeLibrary<ResourceName, Func<IEntityBehaviour>> EntityBehaviours = new("entity_behaviours");

		public ForgeLibraries()
		{
			
		}
	}
}