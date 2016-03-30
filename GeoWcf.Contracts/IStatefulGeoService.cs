using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoWcf.Contracts
{
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    public interface IStatefulGeoService
    {
        [OperationContract]
        void PushZip(string zip);

        [OperationContract]//(IsInitiating = false)]
        ZipCodeData GetZipInfo();

        [OperationContract]//(IsInitiating = false)]
        IEnumerable<ZipCodeData> GetZips(int range);
    }
}
