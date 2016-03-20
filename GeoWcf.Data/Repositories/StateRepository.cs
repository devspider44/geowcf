using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GeoWcf.Core;

namespace GeoWcf.Data
{
    public class StateRepository : DataRepositoryBase<State, GeoWcfDbContext>, IStateRepository
    {
        protected override DbSet<State> DbSet(GeoWcfDbContext entityContext)
        {
            return entityContext.StateSet;
        }

        protected override Expression<Func<State, bool>> IdentifierPredicate(GeoWcfDbContext entityContext, int id)
        {
            return (e => e.StateId == id);
        }

        public State Get(string abbrev)
        {
            using (GeoWcfDbContext entityContext = new GeoWcfDbContext())
            {
                return entityContext.StateSet.FirstOrDefault(e => e.Abbreviation.ToUpper() == abbrev.ToUpper());
            }
        }

        public IEnumerable<State> Get(bool primaryOnly)
        {
            using (GeoWcfDbContext entityContext = new GeoWcfDbContext())
            {
                return entityContext.StateSet.Where(e => e.IsPrimaryState == primaryOnly).ToFullyLoaded();
            }
        }
    }
}
