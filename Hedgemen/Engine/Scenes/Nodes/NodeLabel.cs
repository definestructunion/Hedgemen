using Hgm.Engine.Assets;
using Hgm.Engine.Graphics;
using Hgm.Engine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm.Engine.Scenes.Nodes
{
	public class NodeLabel : Node
	{
		private string text;

		public string Text
		{
			get => text;
			set
			{
				text = value;
				Size = CalculateTextSize();
			}
		}

		public float OutlineWidth { get; set; } = 0.0f;

		public float OutlineHeight { get; set; } = 0.0f;

		private FontStyle fontStyle;

		public FontStyle FontStyle
		{
			get => fontStyle;
			set
			{
				fontStyle = value;
				Size = CalculateTextSize();
			}
		}

		private FontData fontData;

		private ResourceLocation GetCurrentFontResource() => fontData.FromFontStyle(fontStyle);

		private void UpdateFont()
		{
			font.IndexName = fontData.FromFontStyle(fontStyle);
		}
		
		private Font font;

		public Color OutlineColor = Color.Black;

		public NodeLabel(string text, FontData fontData, Scene scene, Node parent) : base(scene, parent)
		{
			this.fontData = fontData;

			font = new Font(this.fontData.RegularResource);

			Text = text;
			Anchor = Anchor.Center;
			Interactable = false;
		}

		protected override void DoUpdate(InputState inputState)
		{
			UpdateFont();
		}

		protected override void DoDraw()
		{
			var layer = 0.0f;
			var position = new Vector2(DrawBounds.X, DrawBounds.Y);
			var origin = new Vector2(0.0f, 0.0f);
			var scale = new Vector2(1.0f, 1.0f);

			var drawData = new PrimitiveDrawStringData
			{
				Font = font,
				Text = Text,
				Color = OutlineColor,
				Origin = origin,
				Scale = scale,
				SpriteEffects = SpriteEffects.None
			};
			
			AttachedScene.Renderer.Begin();

			drawData.Position = position + Vector2.UnitX * OutlineWidth;
			AttachedScene.Renderer.Draw(drawData);
			
			drawData.Position = position - Vector2.UnitX * OutlineWidth;
			AttachedScene.Renderer.Draw(drawData);
			
			drawData.Position = position + Vector2.UnitY * OutlineHeight;
			AttachedScene.Renderer.Draw(drawData);
			
			drawData.Position = position - Vector2.UnitY * OutlineHeight;
			AttachedScene.Renderer.Draw(drawData);
			
			drawData.Position = position;
			drawData.Color = Color;
			AttachedScene.Renderer.Draw(drawData);

			AttachedScene.Renderer.End();
		}

		private Vector2 CalculateTextSize()
		{
			var textMeasurements = font.MeasureString(text);
			return textMeasurements;
		}
	}
}