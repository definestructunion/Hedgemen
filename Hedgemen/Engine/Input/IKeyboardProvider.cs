using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hgm.Input
{
	public interface IKeyboardProvider
	{
		public void Update(GameTime gameTime, Matrix scaleMatrix);

		public string GetTypedChars();
		
		public bool KeyDown(Keys key);
		public bool KeyPressed(Keys key);
		public bool KeyReleased(Keys key);
		
		public bool AnyKeysDown(params Keys[] keys);
		public bool AnyKeysPressed(params Keys[] keys);
		public bool AnyKeysReleased(params Keys[] keys);
	}
}