using Hgm.API.Resources;
using Hgm.Engine.Utilities;

namespace Hgm.API.Areas
{
	public class UAreaTypeInfo : UTypeInfoCreatable<UArea, UAreaArgs>
	{
		public string Name = string.Empty;

		public ResourceName AreaBehaviourName = ResourceName.Empty;
		
		public ResourceName AreaCartographerName = ResourceName.Empty;

		public int Width = 512;

		public int Height = 512;
		
		public override UArea Create(UAreaArgs args)
		{
			args.TypeInfo = this;
			return new UArea(args);
		}
		
		public string GetName() => Name;

		public IAreaBehaviour GetBehaviour() => Hedgemen.Libraries.AreaBehaviours[AreaBehaviourName]();

		public UCartographer GetCartographer() => Hedgemen.Libraries.Cartographers[AreaCartographerName];
	}
}