using Hgm.Engine.Scenes.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hgm.Engine.Scenes
{
	public class SceneMainMenu : Scene
	{
		public SceneMainMenu()
		{
			
		}
		
		protected override void OnInitialize()
		{
			var button = new NodeButton("Hello", this, Root)
			{
				Size = new Vector2(250, 75)
			};

			base.OnInitialize();
		}

		protected override void RegisterHooks()
		{
			base.RegisterHooks();
		}

		protected override void UnregisterHooks()
		{
			base.UnregisterHooks();
		}

		public override void OnUpdate(InputState inputState)
		{
			if(inputState.InputProvider.KeyPressed(Keys.Escape))
				Hedgemen.Game.Exit();
			
			base.OnUpdate(inputState);
		}

		public override void OnDraw()
		{
			base.OnDraw();
		}

		protected override void OnExit()
		{
			base.OnExit();
		}
	}
}