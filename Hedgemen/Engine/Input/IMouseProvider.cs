using Microsoft.Xna.Framework;

namespace Hgm.Input
{
	public interface IMouseProvider
	{
		public void Update(GameTime gameTime, Matrix scaleMatrix);
		
        public Vector2 MousePosition { get; }
		public Vector2 MousePositionDiff { get; }
		 
        public void UpdateMousePosition(Vector2 pos);

		public bool MouseButtonClick(MouseButtons button);
		public bool AnyMouseButtonClick(params MouseButtons[] buttons);
		
		public bool MouseButtonClicked(MouseButtons button);
		public bool AnyMouseButtonClicked(params MouseButtons[] buttons);
		 
		public bool MouseButtonUp(MouseButtons button);
		public bool AnyMouseButtonUp(params MouseButtons[] buttons);

		public bool MouseButtonReleased(MouseButtons button);
		public bool AnyMouseButtonReleased(params MouseButtons[] buttons);
		
		public int MouseWheel { get; }
		public int MouseWheelChange { get; }
	}
}