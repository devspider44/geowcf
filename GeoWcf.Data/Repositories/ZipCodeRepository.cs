using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GeoWcf.Core;

namespace GeoWcf.Data
{
    public class ZipCodeRepository : DataRepositoryBase<ZipCode, GeoWcfDbContext>, IZipCodeRepository
    {
        protected override DbSet<ZipCode> DbSet(GeoWcfDbContext entityContext)
        {
            return entityContext.ZipCodeSet;
        }

        protected override Expression<Func<ZipCode, bool>> IdentifierPredicate(GeoWcfDbContext entityContext, int id)
        {
            return (e => e.ZipCodeId == id);
        }

        public override IEnumerable<ZipCode> Get()
        {
            using (GeoWcfDbContext entityContext = new GeoWcfDbContext())
            {
                return entityContext.ZipCodeSet
                    .Include(e => e.State).ToFullyLoaded();
            }
        }

        public ZipCode GetByZip(string zip)
        {
            using (GeoWcfDbContext entityContext = new GeoWcfDbContext())
            {
                return entityContext.ZipCodeSet
                    .Include(e => e.State)
                    .Where(e => e.Zip == zip)
                    .FirstOrDefault();
            }
        }

        public IEnumerable<ZipCode> GetByState(string state)
        {
            using (GeoWcfDbContext entityContext = new GeoWcfDbContext())
            {
                return entityContext.ZipCodeSet
                    .Include(e => e.State)
                    .Where(e => e.State.Abbreviation == state).ToFullyLoaded();
            }
        }

        public IEnumerable<ZipCode> GetZipsForRange(ZipCode zip, int range)
        {
            using (GeoWcfDbContext entityContext = new GeoWcfDbContext())
            {
                double degrees = range / 69.047;

                return entityContext.ZipCodeSet
                    .Include(e => e.State)
                    .Where(e => (e.Latitude <= zip.Latitude + degrees && e.Latitude >= zip.Latitude - degrees) &&
                                (e.Longitude <= zip.Longitude + degrees && e.Longitude >= zip.Longitude - degrees))
                    .ToFullyLoaded();
            }
        }
    }
}
