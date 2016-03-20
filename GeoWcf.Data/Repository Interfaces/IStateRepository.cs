using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GeoWcf.Core;
using System.Data.Entity;

namespace GeoWcf.Data
{
    public interface IStateRepository : IDataRepository<State>
    {
        State Get(string abbrev);
        IEnumerable<State> Get(bool primaryOnly);
    }
}
