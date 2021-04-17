namespace Hgm.API.Areas
{
	public interface IAreaBehaviour : IBehaviour
	{
		public UArea Self { get; }

		public void Initialize(UArea self);

		public void OnGenerate()
		{
			
		}
	}
}