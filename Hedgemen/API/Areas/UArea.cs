using Hgm.Engine.GameState;

namespace Hgm.API.Areas
{
	public sealed class UArea
	{
		private IAreaBehaviour behaviour;
		
		private GameProperties properties = new();

		public GameProperties Properties => properties;

		public UAreaTypeInfo TypeInfo { get; private set; }

		public string Name => TypeInfo.Name;
		
		public UAreaMap AreaMap { get; private set; }

		private UCartographer cartographer;
		
		public UArea(UAreaArgs args)
		{
			TypeInfo = args.TypeInfo;
			behaviour = TypeInfo.GetBehaviour();
			cartographer = TypeInfo.GetCartographer();
			AreaMap = new UAreaMap(TypeInfo.Width, TypeInfo.Height);
		}

		public void Generate()
		{
			cartographer.Generate(this);
			behaviour.OnCartographerGenerate();
		}
	}

	public struct UAreaArgs
	{
		public UAreaTypeInfo TypeInfo;
	}
}