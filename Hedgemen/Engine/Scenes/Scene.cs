using System;
using Hgm.Engine.Graphics;
using Hgm.Engine.Scenes.Nodes;
using Hgm.Engine.Utilities;
using Hgm.Input;
using Microsoft.Xna.Framework;

namespace Hgm.Engine.Scenes
{
	public abstract class Scene : IExit
	{
		private bool isInitialized = false;
		
		public Color BackgroundColor { get; set; } = Color.CornflowerBlue;
		
		protected InputState InputState;
		
		public GraphicsDeviceManager Graphics { get; private set; }
		
		public IRenderer Renderer { get; private set; }
		
		public NodeRoot Root { get; protected set; }

		protected Scene() : this(Hedgemen.Game.Graphics)
		{
			
		}
		
		protected Scene(GraphicsDeviceManager graphics)
		{
			Graphics = graphics;
		}

		public void Initialize()
		{
			if (isInitialized)
			{
				Console.WriteLine("Scene is already initialized!");
				return;
			}
			
			Root = new NodeRoot(this, new Vector2(1920, 1080));

			Renderer = CreateRenderer();
			InputState = new InputState(new InputProvider());
			
			RegisterHooks();
			OnInitialize();
		}

		protected virtual IRenderer CreateRenderer()
		{
			var rendererArgs = RendererArgs.New();
			rendererArgs.Graphics = Graphics;
			rendererArgs.Matrix = Root.ScaleMatrix;

			return new Renderer(rendererArgs);
		}

		protected virtual void OnInitialize()
		{
			
		}

		protected virtual void RegisterHooks()
		{
			
		}

		protected virtual void UnregisterHooks()
		{
			
		}
		
		public void Update(GameTime gameTime)
		{
			InputState.Update(gameTime, Root.ScaleMatrix);

			InputState.TargetNode = Root.ScanForTargetNode(InputState);
			
			OnUpdate(InputState);
			Root.Update(InputState);
			
			InputState.Reset();
		}

		public void Draw(GameTime gameTime)
		{
			Graphics.GraphicsDevice.Clear(BackgroundColor);
			Root.Draw();
			OnDraw();
		}

		public virtual void OnUpdate(InputState inputState)
		{
			
		}

		public virtual void OnDraw()
		{
			
		}

		protected virtual void OnExit()
		{
			
		}

		public void Exit()
		{
			OnExit();
			UnregisterHooks();
			Root.Discard();
			Renderer.Dispose();
		}
	}
}