using System.Collections.Generic;
using Hgm.Engine.Utilities;
using Newtonsoft.Json;

namespace Hgm.API.Resources
{
	public abstract class UTypeInfo
	{
		public ResourceLocation ResourceName { get; }
		
		[JsonProperty("additional_fields")]
		private Dictionary<ResourceLocation, object> fields = new Dictionary<ResourceLocation, object>();

		public T Add<T>(ResourceLocation resourceName, T val)
		{
			if (fields.ContainsKey(resourceName)) return default;
			fields.Add(resourceName, val);
			return val;
		}

		public T Get<T>(ResourceLocation resourceName)
		{
			return (T)fields.Get(resourceName);
		}
		
		[JsonConstructor]
		protected UTypeInfo()
		{
			
		}
	}
}