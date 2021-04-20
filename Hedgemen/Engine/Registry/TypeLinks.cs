using System;
using System.Collections.Generic;
using Hgm.Engine.Utilities;

namespace Hgm.Engine.Registry
{
    public class TypeLinks<T>
    {
        private IDictionary<string, T> links = new Dictionary<string, T>();

        public void Establish(string linkName, T val)
        {
            if(links.ContainsKey(linkName))
                throw new Exception("Link: " + linkName + " already exists!");
            links.Add(linkName, val);
        }

        public T Get(string linkName)
        {
            return links.Get(linkName);
        }
    }
}