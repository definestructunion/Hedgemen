using Hgm.Engine.Assets;
using Hgm.Engine.Graphics;

namespace Hgm.Engine.Scenes.Nodes
{
	public class NodeButton : Node
	{
		public string Text
		{
			get => Label.Text;
			set => Label.Text = value;
		}
		
		public NodeLabel Label { get; private set; }

		private ButtonData buttonData;

		private Sprite drawingSprite;

		public NodeButton(Scene scene, Node parent) :
			this(string.Empty, Hedgemen.Game.ResourcePack.Button, scene, parent)
		{
			
		}
		
		public NodeButton(string text, Scene scene, Node parent) : 
			this(text, Hedgemen.Game.ResourcePack.Button,
				scene, parent)
		{
			
		}

		public NodeButton(string text, ButtonData button, Scene scene, Node parent) : base(scene, parent)
		{
			buttonData = button;

			Label = new NodeLabel(text, Hedgemen.Game.ResourcePack.FontDefault, scene, this);
			Label.OutlineWidth = 2.0f;
			Label.OutlineHeight = 2.0f;

			drawingSprite = new Sprite(buttonData.GetButton(ButtonState.Regular).Resource);
		}

		protected override void DoUpdate(InputState inputState)
		{
			base.DoUpdate(inputState);
		}
		
		protected override void DoDraw()
		{
			TextureData button = Hedgemen.Game.ResourcePack.Button.GetButton(NodeState);
			drawingSprite.ResourceName = button.Resource;
			
			AttachedScene.Renderer.Begin();
			AttachedScene.Renderer.DrawFrame(new PrimitiveDrawData
			{
				Sprite = drawingSprite,
				DestRect = DrawBounds,
				Color = Color
			}, button.FrameWidth * 3, button.FrameHeight * 3, button.FrameWidth, button.FrameHeight);
			// todo dont *3
			AttachedScene.Renderer.End();
		}
	}
}