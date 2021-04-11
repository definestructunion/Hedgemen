namespace Hgm.Engine.GameState
{
	public interface IGameObject<T, TC>
	{
		public TK Get<TK>() where TK : TC;
		public TK Add<TK>() where TK : TC, new();
	}
}