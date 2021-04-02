using System;
using Newtonsoft.Json;

namespace Hgm.Engine.Utilities
{
	[Serializable]
	public struct ResourceLocation
	{
		public static ResourceLocation Empty => new ResourceLocation(EmptyNamespace, EmptyName);

		public static string EmptyNamespace => "any";

		public static string EmptyName => "null";

		public bool IsEmpty => Equals(Empty);
		
		private string ns;

		private string name;

		public string Namespace
		{
			get
			{
				ns ??= EmptyNamespace;
				return ns;
			}
			set => ns = value;
		}
		
		public string Name
		{
			get
			{
				name ??= EmptyName;
				return name;
			}
			set => name = value;
		}

		public ResourceLocation(string resourceNamespace, string name)
		{
			ns = resourceNamespace;
			this.name = name;
		}

		[JsonConstructor]
		public ResourceLocation(string resource)
		{
			if (resource == null)
			{
				ns = EmptyNamespace;
				name = EmptyName;
				return;
			}
			
			var names = resource.Split(':');

			if (names.Length == 2)
			{
				ns = names[0];
				name = names[1];
			}

			else
			{
				ns = EmptyNamespace;
				name = EmptyName;
			}
		}

		[JsonProperty("resource")]
		public string FullName => Namespace + ':' + Name;

		public override string ToString()
		{
			return FullName;
		}

		public ResourceLocation ToGeneric()
		{
			return new ResourceLocation(EmptyNamespace, Name);
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;

			ResourceLocation resource = (ResourceLocation) obj;
			return FullName.Equals(resource.FullName);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static implicit operator ResourceLocation(string str) => new ResourceLocation(str);

		public static implicit operator string(ResourceLocation resource) => resource.FullName;
	}
}