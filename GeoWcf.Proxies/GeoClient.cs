using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GeoWcf.Contracts;

namespace GeoWcf.Proxies
{
    public class GeoClient :ClientBase<IGeoService>, IGeoService

{
    public ZipCodeData GetZipinfo(string zip)
    {
        return Channel.GetZipinfo(zip);
    }

    public IEnumerable<string> GetStates(bool primaryOnly)
    {
        return Channel.GetStates((primaryOnly));
    }

    public IEnumerable<ZipCodeData> GetZips(string state)
    {
        return Channel.GetZips(state);
    }

    public IEnumerable<ZipCodeData> GetZips(string zip, int range)
    {
        return Channel.GetZips(zip, range);
    }
}
}
