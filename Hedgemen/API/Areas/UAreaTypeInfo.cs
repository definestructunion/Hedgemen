using Hgm.API.Resources;
using Hgm.Engine.Utilities;
using Newtonsoft.Json;

namespace Hgm.API.Areas
{
	public sealed class UAreaTypeInfo : UTypeInfo
	{
		public ResourceName AreaBehaviourName;

		public ResourceName AreaMapName;

		public string Name;

		public int Width;

		public int Height;
	}
}