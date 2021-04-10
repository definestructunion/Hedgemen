namespace Hgm.Engine.GameState
{
	public interface IGameObject<T, TC>
	{
		public TK Get<TK>() where TK : TC;
	}
}