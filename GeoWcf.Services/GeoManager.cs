﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeoWcf.Contracts;
using GeoWcf.Data;

namespace GeoWcf.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class GeoManager : IGeoService
    {

        private IZipCodeRepository _ZipCodeRepository;
        private IStateRepository _StateRepository;
        public GeoManager()
        {
            
        }

        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IStateRepository stateRepository)
        {
            _StateRepository = stateRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
            _StateRepository = stateRepository;
        }

        public ZipCodeData GetZipInfo(string zip)
        {

            ZipCodeData zipCodeData = null;

            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);

            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }
            else
            {
                ApplicationException ex = new ApplicationException(string.Format("Zip code {0} not found.",zip));
                throw new FaultException<ApplicationException>(ex, "Just another message");
            }

            return zipCodeData;

        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            List<string> stateData = new List<string>();

            IStateRepository stateRepository = _StateRepository ?? new StateRepository();

            IEnumerable<State> states = stateRepository.Get(primaryOnly);

            if (states != null)
            {
                foreach (var state in states)
                {
                    stateData.Add(state.Abbreviation);
                }
            }
            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            var zips = zipCodeRepository.GetByState(state);

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


            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ??  new ZipCodeRepository();


            ZipCode zipEntity = zipCodeRepository.GetByZip(zip);
            IEnumerable<ZipCode> zips = zipCodeRepository.GetZipsForRange(zipEntity, range);

            if (zips != null)
            {
                foreach (var zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeData
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip

                    });
                }
            }

            return zipCodeData;
        }

    
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateZipCity(string zip, string city)
        {
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipEntity = zipCodeRepository.GetByZip(zip);
            
            if (zipEntity != null)
            {
                zipEntity.City = city;
                zipCodeRepository.Update(zipEntity);
            }

        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateZipCity(IEnumerable<ZipCityData> zipCityData)
        {
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            //Dictionary<string,string> cityBatch = new Dictionary<string, string>();

            //foreach (ZipCityData zipCityItem in zipCityData) 
            //{
            //    cityBatch.Add(zipCityItem.ZipCode,zipCityItem.City);
            //}

            //zipCodeRepository.UpdateCityBatch(cityBatch);

            int counter = 0;

            foreach (var zipCityItem in zipCityData)
            {
                counter++;

                ZipCode zipEntity = zipCodeRepository.GetByZip(zipCityItem.ZipCode);

                if (zipEntity != null)
                {
                    zipEntity.City = zipCityItem.City;
                    zipCodeRepository.Update(zipEntity);
                    ZipCode updateItem = zipCodeRepository.Update(zipEntity);
                }
            }
        }

    }

   

}
