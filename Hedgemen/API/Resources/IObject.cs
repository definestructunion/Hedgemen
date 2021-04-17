namespace Hgm.API.Resources
{
	public interface IObject<T, TK> where TK : IComponent<T>
	{
		public TC Add<TC>() where TC : TK, new();
		public TC Get<TC>() where TC : TK, new();
	}
}