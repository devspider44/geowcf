using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GeoWcf.Core;
using System.Data.Entity;

namespace GeoWcf.Data
{
    public interface IZipCodeRepository : IDataRepository<ZipCode>
    {
        ZipCode GetByZip(string zip);
        IEnumerable<ZipCode> GetByState(string state);
        IEnumerable<ZipCode> GetZipsForRange(ZipCode zip, int range);
        void UpdateCityBatch(Dictionary<string, string> cityBatch);
    }
}
