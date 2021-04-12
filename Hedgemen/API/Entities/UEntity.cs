using System;
using Hgm.Engine.GameState;

namespace Hgm.API.Entities
{
	public class UEntity : IGameObject
	{
		public GameProperties Properties => properties;

		public EntitySheet Sheet => sheet;
		
		private IEntityBehaviour behaviour;

		/// <summary>
		/// Should only be called for serialization and activation
		/// </summary>
		private UEntity()
		{
			
		}

		public UEntity(UEntityArgs args)
		{
			var rng = new Random();
			behaviour = Hedgemen.Libraries.EntityBehaviours[args.TypeInfo.EntityBehaviourName]();
			Sheet.EntityName = args.TypeInfo.DefaultNames[rng.Next(0, args.TypeInfo.DefaultNames.Count)];
		}

		private GameProperties properties = new();
		private EntitySheet sheet = new();
	}

	public struct UEntityArgs
	{
		public UEntityTypeInfo TypeInfo;
	}
}