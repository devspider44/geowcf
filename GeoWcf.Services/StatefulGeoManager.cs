using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GeoWcf.Contracts;
using GeoWcf.Data;

namespace GeoWcf.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class StatefulGeoManager : IStatefulGeoService
    {
        private ZipCode _ZipCodeEntity;
        public void PushZip(string zip)
        {
            IZipCodeRepository zipCode = new ZipCodeRepository();
            _ZipCodeEntity = zipCode.GetByZip(zip);
        }

        public ZipCodeData GetZipInfo()
        {
            ZipCodeData zipCodeData = null;
            if (_ZipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData
                {
                    City = _ZipCodeEntity.City,
                    State = _ZipCodeEntity.State.Abbreviation,
                    ZipCode = _ZipCodeEntity.Zip
                };
            }
            else
            {
                throw new ApplicationException("uh oh");
            }


            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

            if (_ZipCodeEntity != null)
            {

                IZipCodeRepository zipCodeRepository =new ZipCodeRepository();

                var zips = zipCodeRepository.GetZipsForRange(_ZipCodeEntity, range);

                if (zips != null)
                {
                    foreach (ZipCode zipCode in zips)
                    {
                        zipCodeData.Add(new ZipCodeData
                        {
                            City = zipCode.City,
                            State = zipCode.State.Abbreviation,
                            ZipCode = zipCode.Zip
                        });
                    }
                }
            }

            return zipCodeData;
        }
    }
}
