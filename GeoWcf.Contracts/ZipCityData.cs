﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeoWcf.Contracts
{s
    [DataContract]
    public class ZipCityData
    {
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
    }
}
