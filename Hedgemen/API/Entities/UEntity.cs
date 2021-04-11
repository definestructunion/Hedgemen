using System;
using System.Collections.Generic;
using Hgm.Engine.GameState;
using Hgm.Engine.Utilities;

namespace Hgm.API.Entities
{
	public class UEntity : IGameObject<UEntity, IEntityComponent>
	{
		private Dictionary<Type, IEntityComponent> components = new ();

		public UEntity()
		{
			
		}
		
		public TK Get<TK>() where TK : IEntityComponent
		{
			return (TK) components.Get(typeof(TK));
		}

		public TK Add<TK>() where TK : IEntityComponent, new()
		{
			if (components.ContainsKey(typeof(TK))) return default;
			var component = new TK();
			components.Add(typeof(TK), component);
			return component;
		}
	}
}