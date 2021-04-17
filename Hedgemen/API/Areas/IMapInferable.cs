using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.API.Areas
{
	public interface IMapInferable
	{
		public List<ResourceName> GetTextureNames();
	}
}