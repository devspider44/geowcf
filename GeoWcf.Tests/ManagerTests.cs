using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWcf.Contracts;
using GeoWcf.Data;
using GeoWcf.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GeoWcf.Tests
{
   [TestClass]
    public class ManagerTests
    {
       [TestMethod]
       public void test_zip_code_retrieval()
       {
           Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();

           ZipCode zipCode = new ZipCode
           {
               City = "LINCOL PARK",
               State = new State {Abbreviation = "NJ"},
               Zip = "07035"
           };

           mockZipCodeRepository.Setup(obj => obj.GetByZip("07035")).Returns(zipCode);

           IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);

           ZipCodeData data = geoService.GetZipInfo("07035");

           Assert.IsTrue(data.City.ToUpper() == "LINCOL PARK");
           Assert.IsTrue(data.State == "NJ");
       }
    }
}
