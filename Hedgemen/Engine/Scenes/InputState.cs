using Hgm.Engine.Scenes.Nodes;
using Hgm.Input;
using Microsoft.Xna.Framework;

namespace Hgm.Engine.Scenes
{
	public class InputState
	{
		public InputProvider InputProvider { get; set; }
		
		public Node TargetNode { get; set; }

		public Node PreviousTargetNode { get; private set; }

		public GameTime GameTime { get; private set; } = new GameTime();
		
		public InputState() : this(new InputProvider())
		{
			
		}

		public InputState(InputProvider inputProvider)
		{
			InputProvider = inputProvider;
		}

		public void Update(GameTime gameTime, Matrix scaleMatrix)
		{
			GameTime = gameTime;
			InputProvider.Update(gameTime, scaleMatrix);
		}

		public void Reset()
		{
			PreviousTargetNode = TargetNode;
			TargetNode = null;
		}
	}
}