using Hgm.Engine.Utilities;

namespace Hgm.Engine.Assets
{
	public struct TextureData
	{
		public ResourceLocation Resource { get; private set; }
		
		public int FrameWidth { get; private set; }
		
		public int FrameHeight { get; private set; }

		public TextureData(ResourceLocation resource, int frameWidth = 0, int frameHeight = 0)
		{
			Resource = resource;
			FrameWidth = frameWidth;
			FrameHeight = frameHeight;
		}
	}
}