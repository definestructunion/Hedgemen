using System;
using Hgm.API.Areas;
using Hgm.API.Entities;
using Hgm.Engine.Utilities;

namespace Hgm.API.Modding
{
	public sealed class ForgeLibraries
	{
		public ForgeLibrary<UAreaTypeInfo> AreaTypes { get; private set; } = new (null);
		public ForgeLibrary<ResourceName, Func<IAreaBehaviour>> AreaBehaviours { get; private set; } = new(() => new AreaBehaviourDefault());

		public ForgeLibrary<UEntityTypeInfo> EntityTypes { get; private set; } = new(null);
		public ForgeLibrary<ResourceName, Func<IEntityBehaviour>> EntityBehaviours { get; private set; } = new(() => new EntityBehaviourDefault());

		public ForgeLibrary<UCartographer> Cartographers { get; private set; } = new(new UCartographer());
		public ForgeLibrary<ResourceName, Func<Landscaper>> Landscapers { get; private set; } = new(() => new LandscaperDefault());
		
		public ForgeLibraries()
		{
			
		}
	}
}