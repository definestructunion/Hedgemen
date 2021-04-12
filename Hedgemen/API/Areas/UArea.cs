namespace Hgm.API.Areas
{
	public sealed class UArea
	{
		private IAreaBehaviour behaviour;

		private UCell[,] cells;
		
		private UArea()
		{
			
		}

		public UArea(UAreaArgs args)
		{
			cells = new UCell[args.TypeInfo.Width, args.TypeInfo.Height];
			behaviour = Hedgemen.Libraries.AreaBehaviours[args.TypeInfo.AreaBehaviourName]();
		}
	}

	public struct UAreaArgs
	{
		public UAreaTypeInfo TypeInfo;
	}
}