using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm.Engine.Graphics
{
	public class UnsafeTextureInfo
	{
		public GraphicsDeviceManager Graphics;
		public int Width = 0;
		public int Height = 0;
		public Color[] ColorData = null;
		public bool Mipmap = false;
		public SurfaceFormat SurfaceFormat = SurfaceFormat.Color;

		public UnsafeTextureInfo()
		{
			
		}

		internal Texture2D ToTexture()
		{
			Graphics ??= Hedgemen.Game.Graphics;
			var texture = new Texture2D(Graphics.GraphicsDevice, Width, Height, Mipmap, SurfaceFormat);
			texture.SetData(ColorData);
			return texture;
		}
	}
}