using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.API.Resources
{
	public abstract class UTypeInfo<T> : IResource
	{
		public ResourceName ResourceName { get; set; }
		
		private Dictionary<ResourceName, object> fields = new Dictionary<ResourceName, object>();

		public TK Add<TK>(ResourceName resourceName, TK val)
		{
			if (fields.ContainsKey(resourceName)) return default;
			fields.Add(resourceName, val);
			return val;
		}

		public TK Get<TK>(ResourceName resourceName)
		{
			return (TK)fields.Get(resourceName);
		}
		
		protected UTypeInfo()
		{
			
		}

        public override bool Equals(object obj)
        {
			if(obj is UTypeInfo<T> value)
			{
				return ResourceName.Equals(value.ResourceName);
			}

			return false;
        }

        public override int GetHashCode()
        {
            return ResourceName.GetHashCode();
        }
    }
}