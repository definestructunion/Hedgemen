using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hgm.Input
{
	public class InputProvider : IKeyboardProvider, IMouseProvider
	{
		private StringBuilder typedChars;

		private KeyboardState currentKeys;
		private KeyboardState previousKeys;

		private MouseState currentButtons;
		private MouseState previousButtons;

		private Vector2 cursorPosition;
		
		public InputProvider()
		{
			typedChars = new StringBuilder();
			#if XNA_IMPLEMENTATION_FNA
			TextInputEXT.TextInput += c =>
			{
				switch (c)
				{
					case '\b': return;
					case '\n': typedChars.Append('\n'); break;
				}
				
				typedChars.Append(c); 
			};
			#endif
		}
		
		public void Update(GameTime gameTime, Matrix scaleMatrix)
		{
			previousKeys = currentKeys;
			currentKeys = Keyboard.GetState();

			previousButtons = currentButtons;
			currentButtons = Mouse.GetState();

			var mouseState = Mouse.GetState();
			cursorPosition = new Vector2(mouseState.X, mouseState.Y);
			cursorPosition = Vector2.Transform(cursorPosition, Matrix.Invert(scaleMatrix));
		}

		public string GetTypedChars()
		{
			string chars = typedChars.ToString();
			typedChars.Clear();
			return chars;
		}

		public bool KeyDown(Keys key)
		{
			return currentKeys.IsKeyDown(key);
		}

		public bool KeyPressed(Keys key)
		{
			return previousKeys.IsKeyDown(key) && !currentKeys.IsKeyDown(key);
		}

		public bool KeyReleased(Keys key)
		{
			return currentKeys.IsKeyUp(key);
		}

		public bool AnyKeysDown(params Keys[] keys)
		{
			return keys.Any(KeyDown);
		}

		public bool AnyKeysPressed(params Keys[] keys)
		{
			return keys.Any(KeyPressed);
		}

		public bool AnyKeysReleased(params Keys[] keys)
		{
			return keys.Any(KeyReleased);
		}

		public Vector2 MousePosition => cursorPosition;
		
		public Vector2 MousePositionDiff => new Vector2(0, 0); // todo
		
		public void UpdateMousePosition(Vector2 pos)
		{
			cursorPosition = pos;
		}

		public bool MouseButtonClick(MouseButtons button)
		{
			return MouseStateFired(currentButtons, button, ButtonState.Pressed);
		}

		public bool AnyMouseButtonClick(params MouseButtons[] buttons)
		{
			return buttons.Any(e => MouseStateFired(currentButtons, e, ButtonState.Pressed));
		}

		public bool MouseButtonClicked(MouseButtons button)
		{
			return MouseStateFired(previousButtons, button, ButtonState.Pressed) &&
					!MouseStateFired(currentButtons, button, ButtonState.Pressed);
		}

		public bool AnyMouseButtonClicked(params MouseButtons[] buttons)
		{
			return buttons.Any(e => 
				MouseStateFired(previousButtons, e, ButtonState.Pressed) && !MouseStateFired(currentButtons, e, ButtonState.Pressed));
		}

		public bool MouseButtonUp(MouseButtons button)
		{
			return MouseStateFired(currentButtons, button, ButtonState.Released);
		}

		public bool AnyMouseButtonUp(params MouseButtons[] buttons)
		{
			return buttons.Any(e => 
				MouseStateFired(previousButtons, e, ButtonState.Released) && 
				!MouseStateFired(currentButtons, e, ButtonState.Released));
		}

		public bool MouseButtonReleased(MouseButtons button)
		{
			return MouseStateFired(currentButtons, button, ButtonState.Released) && MouseStateFired(previousButtons, button, ButtonState.Pressed);
		}

		public bool AnyMouseButtonReleased(params MouseButtons[] buttons)
		{
			return buttons.Any(e => 
				MouseStateFired(currentButtons, e, ButtonState.Released) && MouseStateFired(previousButtons, e, ButtonState.Pressed));
		}

		public int MouseWheel => currentButtons.ScrollWheelValue;

		public int MouseWheelChange => Math.Abs(currentButtons.ScrollWheelValue - previousButtons.ScrollWheelValue);

		private bool MouseStateFired(MouseState mouseState, MouseButtons button, ButtonState state)
		{
			switch (button)
			{
				case MouseButtons.LeftButton: return mouseState.LeftButton == state;
				case MouseButtons.MiddleButton: return mouseState.MiddleButton == state;
				case MouseButtons.RightButton: return mouseState.RightButton == state;
				case MouseButtons.XButton1: return mouseState.XButton1 == state;
				case MouseButtons.XButton2: return mouseState.XButton2 == state;
			}

			return false;
		}
	}
}