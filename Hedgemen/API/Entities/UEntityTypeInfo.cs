using System.Collections.Generic;
using Hgm.API.Resources;
using Hgm.Engine.Utilities;

namespace Hgm.API.Entities
{
    public class UEntityTypeInfo : UTypeInfo
    {
        public ResourceName EntityBehaviourName = ResourceName.Empty;

        public List<string> DefaultNames = new ();
    }
}