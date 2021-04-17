namespace Hgm.API.Areas
{
	public class AreaBehaviourDefault : IAreaBehaviour
	{
		public UArea Self { get; private set; }
		
		public void Initialize(UArea self)
		{
			Self = self;
		}
	}
}