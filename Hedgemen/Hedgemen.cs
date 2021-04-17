using Hgm.API.Modding;
using Hgm.Engine;

namespace Hgm
{
	public static class Hedgemen
	{
		public static IGame Game { get; private set; }

		public static ForgeLibraries Libraries { get; private set; }

		public static HedgemenForge Forge { get; private set; }
		
		public static HedgemenRng Rng { get; private set; }
		
		public static void HedgemenStart(HedgemenArgs args)
		{
			Game = args.Game;
			Libraries = new ForgeLibraries();
			Forge = new HedgemenForge();
			Rng = new HedgemenRng();
			
			Forge.ForgeStart(args.ForgeArgs);
		}
	}
	
	public struct HedgemenArgs
	{
		public IGame Game;
		
		public HedgemenForgeArgs ForgeArgs;
	}
}