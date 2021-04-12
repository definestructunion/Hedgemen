using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.Engine.GameState
{
    public sealed class GameProperties
    {
        private Dictionary<ResourceName, IGameProperty> properties = new();

        public T Add<T>(ResourceName resourceName, T val) where T : IGameProperty
        {
            if (properties.ContainsKey(resourceName)) return default;
            properties.Add(resourceName, val);
            return val;
        }

        public T Get<T>(ResourceName resourceName) where T : IGameProperty
        {
            return (T)properties.Get(resourceName);
        }

        public T Replace<T>(ResourceName resourceName, T val) where T : IGameProperty
        {
            if (!properties.ContainsKey(resourceName)) return default;
            properties.Remove(resourceName);
            properties.Add(resourceName, val);
            return val;
        }
    }
}