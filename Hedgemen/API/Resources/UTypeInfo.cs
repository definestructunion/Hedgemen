using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.API.Resources
{
	public abstract class UTypeInfo : IResource
	{
		public ResourceName ResourceName { get; set; }
		
		private Dictionary<ResourceName, object> fields = new Dictionary<ResourceName, object>();

		public T Add<T>(ResourceName resourceName, T val)
		{
			if (fields.ContainsKey(resourceName)) return default;
			fields.Add(resourceName, val);
			return val;
		}

		public T Get<T>(ResourceName resourceName)
		{
			return (T)fields.Get(resourceName);
		}
		
		protected UTypeInfo()
		{
			
		}
	}
}