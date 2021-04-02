using System;
using Microsoft.Xna.Framework;

namespace Hgm.Engine.Scenes.Nodes
{
	public class NodeRoot : Node
	{
		public Vector2 TargetResolution { get; set; }

		public Vector2 ActualResolution => GetScreenDimensions(AttachedScene);

		public Vector2 ResolutionScale => new Vector2(ActualResolution.X / TargetResolution.X, ActualResolution.Y / TargetResolution.Y);

		public Matrix ScaleMatrix => Matrix.CreateScale(ResolutionScale.X, ResolutionScale.Y, 1.0f);
		
		public NodeRoot(Scene scene, Vector2 targetResolution) : base(scene, null)
		{
			Visible = false;
			ChildrenAreVisible = true;
			TargetResolution = targetResolution;
			Bounds = GetScreenBounds(AttachedScene);
		}

		public override void Update(InputState inputState)
		{
			UpdateChildren(inputState);
		}

		public Vector2 ScaleToResolution(Vector2 size)
		{
			var difference = ResolutionScale;

			size.X = size.X * difference.X;
			size.Y = size.Y * difference.Y;

			return size;
		}
		
		public Rectangle ScaleToResolution(Rectangle bounds)
		{
			var difference = ResolutionScale;

			bounds.X = (int)Math.Round(bounds.X * difference.X);
			bounds.Y = (int)Math.Round(bounds.Y * difference.Y);
			
			bounds.Width = (int)Math.Round(bounds.Width * difference.X);
			bounds.Height = (int)Math.Round(bounds.Height * difference.Y);

			return bounds;
		}

		private static Rectangle GetScreenBounds(Scene scene)
		{
			return scene.Graphics.GraphicsDevice.Viewport.Bounds;
		}

		private static Vector2 GetScreenDimensions(Scene scene)
		{
			var bounds = GetScreenBounds(scene);
			return new Vector2(bounds.Width, bounds.Height);
		}

		public override Rectangle CalculateBounds()
		{
			return new Rectangle(0, 0, (int)TargetResolution.X, (int)TargetResolution.Y);
		}
	}
}