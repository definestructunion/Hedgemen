using Hgm.Engine.Utilities;

namespace Hgm.Engine.Assets
{
	public struct FontData
	{
		public ResourceLocation RegularResource { get; private set; }
		
		public ResourceLocation BoldResource { get; private set; }
		
		public ResourceLocation ItalicsResource { get; private set; }
		
		public FontData(ResourceLocation regularResource, ResourceLocation boldResource, ResourceLocation italicsResource)
		{
			RegularResource = regularResource;
			BoldResource = boldResource;
			ItalicsResource = italicsResource;
		}
		
		public ResourceLocation FromFontStyle(FontStyle fontStyle)
		{
			switch (fontStyle)
			{
				case FontStyle.Regular: return RegularResource;
				case FontStyle.Bold: return BoldResource;
				case FontStyle.Italics: return ItalicsResource;
				default: return RegularResource;
			}
		}
		
		public FontStyle ToFontStyle(ResourceLocation fontResource)
		{
			if (fontResource.Equals(RegularResource)) return FontStyle.Regular;
			if (fontResource.Equals(BoldResource)) return FontStyle.Bold;
			if (fontResource.Equals(ItalicsResource)) return FontStyle.Italics;

			return FontStyle.Regular;
		}

		public FontData(ResourceLocation resource)
		{
			RegularResource = resource;
			BoldResource = resource;
			ItalicsResource = resource;
		}
	}
}