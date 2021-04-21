using Hgm.Engine.Assets;
using Hgm.Engine.Utilities;
using Microsoft.Xna.Framework;

namespace Hgm
{
	public interface IGame : IExit
	{
		public GraphicsDeviceManager Graphics { get; }
		public AssetManager Assets { get; }
		public ResourcePack ResourcePack { get; }
	}
}