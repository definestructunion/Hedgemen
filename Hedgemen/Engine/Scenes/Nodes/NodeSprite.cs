using Hgm.Engine.Graphics;
using Hgm.Engine.Utilities;

namespace Hgm.Engine.Scenes.Nodes
{
	public class NodeSprite : Node
	{
		private Sprite sprite;
		
		public NodeSprite(ResourceLocation resource, Scene scene, Node parent) : base(scene, parent)
		{
			sprite = new Sprite(resource);
		}

		protected override void DoDraw()
		{
			AttachedScene.Renderer.Begin();
			AttachedScene.Renderer.Draw(new PrimitiveDrawData
			{
				Sprite = sprite,
				DestRect = DrawBounds,
				Color = Color
			});
			AttachedScene.Renderer.End();
		}
	}
}