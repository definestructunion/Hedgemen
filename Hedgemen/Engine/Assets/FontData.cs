using Hgm.Engine.Utilities;

namespace Hgm.Engine.Assets
{
	public struct FontData
	{
		public ResourceName RegularResource { get; private set; }
		
		public ResourceName BoldResource { get; private set; }
		
		public ResourceName ItalicsResource { get; private set; }
		
		public FontData(ResourceName regularResource, ResourceName boldResource, ResourceName italicsResource)
		{
			RegularResource = regularResource;
			BoldResource = boldResource;
			ItalicsResource = italicsResource;
		}
		
		public ResourceName FromFontStyle(FontStyle fontStyle)
		{
			switch (fontStyle)
			{
				case FontStyle.Regular: return RegularResource;
				case FontStyle.Bold: return BoldResource;
				case FontStyle.Italics: return ItalicsResource;
				default: return RegularResource;
			}
		}
		
		public FontStyle ToFontStyle(ResourceName fontResource)
		{
			if (fontResource.Equals(RegularResource)) return FontStyle.Regular;
			if (fontResource.Equals(BoldResource)) return FontStyle.Bold;
			if (fontResource.Equals(ItalicsResource)) return FontStyle.Italics;

			return FontStyle.Regular;
		}

		public FontData(ResourceName resource)
		{
			RegularResource = resource;
			BoldResource = resource;
			ItalicsResource = resource;
		}
	}
}