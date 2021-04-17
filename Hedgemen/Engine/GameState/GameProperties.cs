using System;
using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.Engine.GameState
{
    public sealed class GameProperties
    {
        private Dictionary<Type, IGameProperty> properties = new();
        private List<IGameProperty> propertiesList = new();

        public IReadOnlyList<IGameProperty> Properties => propertiesList;
        
        public T Add<T>() where T : IGameProperty, new()
        {
            var type = typeof(T);
            if (properties.ContainsKey(type)) return default;
            var property = new T();
            properties.Add(type, property);
            propertiesList.Add(property);
            return property;
        }

        public void Add(IGameProperty property)
        {
            if (properties.ContainsKey(property.GetType())) return;
            properties.Add(property.GetType(), property);
            propertiesList.Add(property);
        }

        public T Get<T>() where T : IGameProperty
        {
            return (T)properties.Get(typeof(T));
        }

        public T GetFirst<T>()
        {
            foreach (var property in Properties)
            {
                if (property is T propertyT)
                    return propertyT;
            }

            return default;
        }

        public List<T> GetAll<T>()
        {
            // maybe set a capacity to avoid array reallocations
            var list = new List<T>();
            foreach (var property in Properties)
            {
                if(!(property is T)) continue;
                list.Add((T)property);
            }

            return list;
        }

        public void Perform<T>(Action<T> action)
        {
            foreach (var property in Properties)
            {
                if (property is T propertyT)
                    action(propertyT);
            }
        }
    }
}