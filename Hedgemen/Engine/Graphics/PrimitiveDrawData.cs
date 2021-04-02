using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm.Engine.Graphics
{
	public struct PrimitiveDrawData
	{
		private Sprite sprite;
		private Rectangle? srcRect;
		private Rectangle destRect;
		private Color? color;
		private float rotation;
		private Vector2? origin;
		private Vector2? scale;
		private SpriteEffects? spriteEffects;
		private float layerDepth;

		public Sprite Sprite
		{
			get => sprite;
			set => sprite = value;
		}

		public Rectangle? SrcRect
		{
			get => srcRect;
			set => srcRect = value;
		}

		public Rectangle DestRect
		{
			get => destRect;
			set => destRect = value;
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