﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoWcf.Contracts
{
    [ServiceContract]
    public interface IGeoService
    {
        [OperationContract]
        ZipCodeData GetZipinfo(string zip);

        [OperationContract]
        IEnumerable<string> GetStates(bool primaryOnly);

        [OperationContract(Name="GetZipsByState")]
        IEnumerable<ZipCodeData> GetZips(string state);

        [OperationContract(Name="GetZipsForRange")]
        IEnumerable<ZipCodeData> GetZips(string zip, int range);
    }
}
