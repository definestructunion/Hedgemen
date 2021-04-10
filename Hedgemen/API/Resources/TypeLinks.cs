using System;
using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.API.Resources
{
	public sealed class TypeLinks<T>
	{
		private Dictionary<string, T> links = new();

		public string TypeName { get; private set; } = string.Empty;

		public TypeLinks(string typeName)
		{
			TypeName = typeName;
		}

		public void Establish(string name, T val)
		{
			if (links.ContainsKey(name)) throw new Exception("Link of name " + name + "already exists!");
			links.Add(name, val);
		}

		public T Get(string name)
		{
			return links.Get(name);
		}
	}
}