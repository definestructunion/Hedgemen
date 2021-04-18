using System;
using System.Collections.Generic;
using System.Linq;
using Hgm.API.Resources;
using Hgm.Engine.Utilities;

namespace Hgm.API.Modding
{
	public class ForgeLibrary<T> where T : IResource
	{
		private Dictionary<ResourceName, T> items = new ();

		private T defaultValue = default;
		
		public ForgeLibrary(T defaultValue)
		{
			this.defaultValue = defaultValue;
		}

		public void Register(T item)
		{
			if (items.ContainsKey(item.ResourceName)) return;
			items.Add(item.ResourceName, item);
		}

		public void Unregister(ResourceName resource)
		{
			if (!items.ContainsKey(resource)) return;
			items.Remove(resource);
		}

		public T this[ResourceName resource]
		{
			get => Get(resource);
		}

		public T Get(ResourceName resource)
		{
			if (!items.ContainsKey(resource)) return defaultValue;
			return items.Get(resource);
		}

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
	
	public class ForgeLibrary<T, TK>
	{
		private Dictionary<T, TK> items = new ();

		private TK defaultValue;
		
		public ForgeLibrary(TK defaultValue)
		{
			this.defaultValue = defaultValue;
		}

		public void Register(T resourceName, TK item)
		{
			if (items.ContainsKey(resourceName)) return;
			items.Add(resourceName, item);
		}

		public void Unregister(T resource)
		{
			if (!items.ContainsKey(resource)) return;
			items.Remove(resource);
		}
		
		public TK this[T resource]
		{
			get
			{
				if (!items.ContainsKey(resource)) return defaultValue;
				return items.Get(resource);
			}
		}

		public IReadOnlyList<TK> Entries => items.Values.ToList();

		public void Loop(Action<TK> action)
		{
			foreach (var entry in Entries)
			{
				action(entry);
			}
		}

		public List<TK> ToList()
		{
			return items.Values.ToList();
		}
	}
}