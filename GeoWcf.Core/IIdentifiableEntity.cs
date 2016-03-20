using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoWcf.Core
{
    public interface IIdentifiableEntity
    {
        int EntityId { get; set; }
    }
}
