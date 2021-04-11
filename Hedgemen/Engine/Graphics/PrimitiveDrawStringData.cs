using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm.Engine.Graphics
{
	public struct PrimitiveDrawStringData
	{
		private string text;
		private Font font;
		private Vector2 position;
		private Color? color;
		private float rotation;
		private Vector2? origin;
		private Vector2? scale;
		private SpriteEffects? spriteEffects;
		private float layerDepth;

		public Font Font
		{
			get => font;
			set => font = value;
		}

		public string Text
		{
			get
			{
				text ??= string.Empty;
				return text;
			}

			set => text = value;
		}
        
		public Vector2 Position
		{
			get => position;
			set => position = value;
		}

		public Color Color
		{
			get
			{
				color ??= Color.White;
				return color.Value;
			}

			set => color = value;
		}

		public float Rotation
		{
			get => rotation;
			set => rotation = value;
		}

		public Vector2 Origin
		{
			get
			{
				origin ??= Vector2.Zero;
				return origin.Value;
			}

			set => origin = value;
		}

		public Vector2 Scale
		{
			get
			{
				scale ??= Vector2.One;
				return scale.Value;
			}

			set => scale = value;
		}

		public SpriteEffects SpriteEffects
		{
			get
			{
				spriteEffects ??= SpriteEffects.None;
				return spriteEffects.Value;
			}

			set => spriteEffects = value;
		}

		public float LayerDepth
		{
			get => layerDepth;
			set => layerDepth = value;
		}
	}
}