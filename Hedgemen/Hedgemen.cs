using Hgm.API.Modding;

namespace Hgm
{
	public static class Hedgemen
	{
		public static IGame Game { get; private set; }

		public static ForgeLibraries Libraries { get; private set; } = new();

		public static HedgemenForge Forge { get; private set; } = new ();
		
		public static void HedgemenStart(HedgemenArgs args)
		{
			Game = args.Game;
			
			Forge.ForgeStart(args.ForgeArgs);
		}
	}
	
	public struct HedgemenArgs
	{
		public IGame Game;
		
		public HedgemenForgeArgs ForgeArgs;
	}
}