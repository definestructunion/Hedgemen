namespace Hgm.API.Areas
{
	public sealed class UArea
	{
		private IAreaBehaviour behaviour;

		private UAreaMap map;
		
		private UArea()
		{
			
		}

		public UArea(UAreaArgs args)
		{
			map = new UAreaMap(args.TypeInfo.Width, args.TypeInfo.Height);
			behaviour = Hedgemen.Libraries.AreaBehaviours[args.TypeInfo.AreaBehaviourName]();
		}
	}

	public struct UAreaArgs
	{
		public UAreaTypeInfo TypeInfo;
	}
}