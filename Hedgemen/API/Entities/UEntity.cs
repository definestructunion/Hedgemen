namespace Hgm.API.Entities
{
	public sealed class UEntity
	{
		private IEntityBehaviour behaviour;
		
		public UEntityTypeInfo TypeInfo { get; private set; }

		public string Name { get; private set; } = "Unnamed";
		
		private UEntity()
		{
			
		}

		public UEntity(UEntityArgs args)
		{
			TypeInfo = args.TypeInfo;
			Name = TypeInfo.GetName();
		}
	}

	public struct UEntityArgs
	{
		public UEntityTypeInfo TypeInfo;
	}
}