﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GeoWcf.Contracts;

namespace GeoWcf.Proxies
{
    public class StatefulGeoClient : ClientBase<IStatefulGeoService>, IStatefulGeoService
    {
        public void PushZip(string zip)
        {
            Channel.PushZip(zip);
        }

        public ZipCodeData GetZipInfo()
        {
            return Channel.GetZipInfo();
        }

        public IEnumerable<ZipCodeData> GetZips(int range)
        {
            return Channel.GetZips(range);
        }
    }
}
