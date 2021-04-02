using Hgm.Engine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm.Engine.Graphics
{
	public class Font
	{
		private SpriteFont spriteFont;

		public SpriteFont SpriteFont => spriteFont;

		private ResourceLocation indexName;

		public ResourceLocation IndexName
		{
			get => indexName;
			set
			{
				indexName = value;
				Hedgemen.Game.Assets.Load<SpriteFont>(indexName);
			}
		}

		public Font(ResourceLocation indexName)
		{
			spriteFont = Hedgemen.Game.Assets.Load<SpriteFont>(indexName);
		}

		public Vector2 MeasureString(string text)
		{
			return spriteFont.MeasureString(text);
		}
	}
}