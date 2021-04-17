using Hgm.Engine.Utilities;

namespace Hgm.API.Resources
{
	public abstract class TypeInfo<T> : IResource
	{
		public ResourceName ResourceName { get; set; } = ResourceName.Empty;
	}
}