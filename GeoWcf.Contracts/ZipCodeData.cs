using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeoWcf.Contracts
{
    [DataContract]
    public class ZipCodeData
    {
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }      
    }
}
