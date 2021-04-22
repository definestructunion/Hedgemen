using System.Collections.Generic;
using Hgm.API.Resources;
using Hgm.Engine.Utilities;

namespace Hgm.API.Entities
{
	public class UEntityTypeInfo : UTypeInfoCreatable<UEntity, UEntityArgs>
	{
		public List<string> Names = new ();

		public ResourceName EntityBehaviourName = ResourceName.Empty;
		
		public override UEntity Create(UEntityArgs args)
		{
			args.TypeInfo = this;
			return new UEntity(args);
		}

		public string GetName() => Names[Hedgemen.Rng.EntityRng.Next(0, Names.Count)];

		public IEntityBehaviour GetBehaviour() => Hedgemen.Libraries.EntityBehaviours.Get(EntityBehaviourName)();
	}
}