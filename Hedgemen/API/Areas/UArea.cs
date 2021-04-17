using Hgm.Engine.GameState;

namespace Hgm.API.Areas
{
	public sealed class UArea
	{
		private IAreaBehaviour behaviour;
		
		private GameProperties properties = new();

		public GameProperties Properties => properties;

		public string Name { get; set; } = string.Empty;
		
		public UAreaTypeInfo TypeInfo { get; private set; }
		
		public UAreaMap AreaMap { get; private set; }

		private UCartographer cartographer;
		
		public UArea(UAreaArgs args)
		{
			TypeInfo = args.TypeInfo;
			behaviour = TypeInfo.GetBehaviour();
			cartographer = TypeInfo.GetCartographer();
			Name = TypeInfo.Name;
			AreaMap = new UAreaMap(TypeInfo.Width, TypeInfo.Height);
		}

		public void Generate()
		{
			cartographer.Generate(this);
			behaviour.OnGenerate();
		}
	}

	public struct UAreaArgs
	{
		public UAreaTypeInfo TypeInfo;
	}
}