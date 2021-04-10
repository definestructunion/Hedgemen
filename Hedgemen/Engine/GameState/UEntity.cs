using System;
using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.Engine.GameState
{
	public class UEntity : IGameObject<UEntity, IEntityComponent>
	{
		private Dictionary<Type, IEntityComponent> components = new ();

		public UEntity()
		{
			this.AddComponent(() => new Inventory());
		}
		
		public TK Get<TK>() where TK : IEntityComponent
		{
			return (TK) components.Get(typeof(TK));
		}

		public void AddComponent<TK>(Func<TK> componentCreator) where TK : IEntityComponent
		{
			if (components.ContainsKey(typeof(TK))) return;
			var component = componentCreator();
			components.Add(component.GetType(), component);
		}
	}
}