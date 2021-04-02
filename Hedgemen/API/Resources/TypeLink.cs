using System;
using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.API.Resources
{
	public sealed class TypeLink<T>
	{
		private Dictionary<string, Func<T>> links = new Dictionary<string, Func<T>>();

		public TypeLink()
		{
			
		}

		public void Establish(string name, Func<T> creator)
		{
			if (links.ContainsKey(name)) throw new Exception("Link of name " + name + "already exists!");
			links.Add(name, creator);
		}

		public T Get(string name)
		{
			return links.Get(name)();
		}
	}
}