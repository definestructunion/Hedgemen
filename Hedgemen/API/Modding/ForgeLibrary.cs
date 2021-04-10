using System;
using System.Collections.Generic;
using System.Linq;
using Hgm.API.Resources;
using Hgm.Engine.Utilities;

namespace Hgm.API.Modding
{
	public class ForgeLibrary<T>
	{
		private Dictionary<ResourceName, T> items = new ();

		public string LibraryName { get; set; } = string.Empty;
		
		public ForgeLibrary(string libraryName)
		{
			LibraryName = libraryName;
		}

		public void Register(ResourceName resourceName, T item)
		{
			if (items.ContainsKey(resourceName)) return;
			items.Add(resourceName, item);
		}

		public void Unregister(ResourceName resource)
		{
			if (!items.ContainsKey(resource)) return;
			items.Remove(resource);
		}
		
		public T this[ResourceName resource] => items.Get(resource);

		public IReadOnlyList<T> Entries => items.Values.ToList();

		public void Loop(Action<T> action)
		{
			foreach (var entry in Entries)
			{
				action(entry);
			}
		}

		public List<T> ToList()
		{
			return items.Values.ToList();
		}
	}
}