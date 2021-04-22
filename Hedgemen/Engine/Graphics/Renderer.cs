using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm.Engine.Graphics
{
	public class Renderer : IRenderer
	{
		public SpriteBatch Batch { get; private set; }

		public GraphicsDeviceManager Graphics { get; private set; }
		
		public RasterizerState Rasterizer { get; private set; }
		
		public bool IsPrepped { get; private set; }

		public Rectangle Scissor { get; set; }

		public SpriteSortMode SortMode { get; set; }
		
		public BlendState BlendState { get; set; }
		
		public SamplerState SamplerState { get; set; }
		
		public DepthStencilState DepthStencilState { get; set; }

		public Effect DefaultEffect { get; set; }
		
		public Matrix Matrix { get; set; }
		
		public Renderer(RendererArgs args)
		{
			IsPrepped = false;
			Batch = new SpriteBatch(args.Graphics.GraphicsDevice);

			Graphics = args.Graphics;
			Rasterizer = args.Rasterizer;
			SortMode = args.SortMode;
			BlendState = args.BlendState;
			SamplerState = args.SamplerState;
			DepthStencilState = args.DepthStencilState;
			DefaultEffect = args.DefaultEffect;
			Matrix = args.Matrix;

			Scissor = GetScreenBounds();
		}

		public void Dispose()
		{
			Batch.Dispose();
		}

		private void PrepIfNeeded()
		{
			if (!IsPrepped) Begin();
			IsPrepped = true;
		}

		private void UnprepIfNeeded()
		{
			if(IsPrepped) End();
			IsPrepped = false;
		}

		private void TryPrep(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, 
			DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect,
			Matrix matrix)
		{
			effect ??= DefaultEffect;
			
			if (IsPrepped) return;
			Batch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
			Graphics.GraphicsDevice.ScissorRectangle = Scissor;
			IsPrepped = true;
		}

		private void TryUnprep()
		{
			if (!IsPrepped) return;
			Batch.End();
			IsPrepped = false;
		}

		public void Begin()
		{
			Begin(SortMode, BlendState, SamplerState, DepthStencilState, Rasterizer, DefaultEffect, Matrix);
		}
		
		public void Begin(Effect effect)
		{
			Begin(SortMode, BlendState, SamplerState, DepthStencilState, Rasterizer, effect, Matrix);
		}
		
		public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, 
			DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect,
			Matrix matrix)
		{
			TryPrep(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
		}

		public void End()
		{
			TryUnprep();
			Scissor = GetScreenBounds();
		}

		public Rectangle GetScreenBounds()
		{
			var viewport = Graphics.GraphicsDevice.Viewport;
			return new Rectangle(0, 0, viewport.Width, viewport.Height);
		}
		
		public Rectangle[] CreatePatches(Rectangle rectangle, int leftPadding, int rightPadding, int topPadding, int bottomPadding)
		{
			var x = rectangle.X;
			var y = rectangle.Y;
			var w = rectangle.Width;
			var h = rectangle.Height;
			var middleWidth = w - leftPadding - rightPadding;
			var middleHeight = h - topPadding - bottomPadding;
			var bottomY = y + h - bottomPadding;
			var rightX = x + w - rightPadding;
			var leftX = x + leftPadding;
			var topY = y + topPadding;
			var patches = new[]
			{
				new Rectangle(x, y, leftPadding, topPadding), // top left
				new Rectangle(leftX, y, middleWidth, topPadding), // top middle
				new Rectangle(rightX, y, rightPadding, topPadding), // top right
				new Rectangle(x, topY, leftPadding, middleHeight), // left middle
				new Rectangle(leftX, topY, middleWidth, middleHeight), // middle
				new Rectangle(rightX, topY, rightPadding, middleHeight), // right middle
				new Rectangle(x, bottomY, leftPadding, bottomPadding), // bottom left
				new Rectangle(leftX, bottomY, middleWidth, bottomPadding), // bottom middle
				new Rectangle(rightX, bottomY, rightPadding, bottomPadding) // bottom right
			};
			return patches;
		}

		public void Draw(PrimitiveDrawData data)
		{
			Batch.Draw(data.Sprite.Texture, data.DestRect, data.SrcRect, data.Color, data.Rotation, data.Origin,
				data.SpriteEffects, data.LayerDepth);
		}

		public void Draw(PrimitiveDrawStringData data)
		{
			Batch.DrawString(data.Font.SpriteFont, data.Text, data.Position, data.Color, data.Rotation, data.Origin,
				data.Scale, data.SpriteEffects, data.LayerDepth);
		}

		public void DrawFrame(PrimitiveDrawData data, int paddingDestX, int paddingDestY, int paddingSrcX, int paddingSrcY)
		{
			data.SrcRect ??= data.Sprite.Bounds;
			
			Rectangle[] sourcePatches = CreatePatches(data.SrcRect.Value, paddingSrcX, paddingSrcX,  paddingSrcY, paddingSrcY);
			Rectangle[] destPatches = CreatePatches(data.DestRect, paddingDestX, paddingDestX, paddingDestY, paddingDestY);

			for (int i = 0; i < 9; ++i)
			{
				data.SrcRect = sourcePatches[i];
				data.DestRect = destPatches[i];

				Draw(data);
			}
		}
	}

	public struct RendererArgs
	{
		public static RendererArgs New()
		{
			var args = new RendererArgs
			{
				Graphics = null,
				Rasterizer = new RasterizerState
				{
					CullMode = CullMode.CullCounterClockwiseFace,
					DepthBias = 0.0f,
					FillMode = FillMode.Solid,
					MultiSampleAntiAlias = false,
					ScissorTestEnable = true,
					SlopeScaleDepthBias = 0.0f
				},
				SortMode = SpriteSortMode.Deferred,
				BlendState = BlendState.AlphaBlend,
				SamplerState = SamplerState.PointClamp,
				DepthStencilState = DepthStencilState.None,
				DefaultEffect = null,
				Matrix = Matrix.Identity
			};

			return args;
		}
		
		public GraphicsDeviceManager Graphics { get; set; }

		public RasterizerState Rasterizer { get; set; }
		
		public SpriteSortMode SortMode { get; set; }
		
		public BlendState BlendState { get; set; }
		
		public SamplerState SamplerState { get; set; }
		
		public DepthStencilState DepthStencilState { get; set; }
		
		public Effect DefaultEffect { get; set; }
		
		public Matrix Matrix { get; set; }
	}
}