using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm.Engine.Graphics
{
	public interface IRenderer : IDisposable
	{
		public bool IsPrepped { get; }
		
		public Rectangle Scissor { get; set; }

		public Rectangle GetScreenBounds();
		
		public void End();

		public void Begin();
		
		public void Begin(Effect effect);
		
		public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState,
			DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, 
			Matrix matrix);

		public Rectangle[] CreatePatches(Rectangle rectangle, int leftPadding, int rightPadding, int topPadding,
			int bottomPadding);

		public void DrawFrame(PrimitiveDrawData data, int paddingDestX, int paddingDestY, int paddingSrcX, int paddingSrcY);

		public void Draw(PrimitiveDrawData data);

		public void Draw(PrimitiveDrawStringData data);
	}
}