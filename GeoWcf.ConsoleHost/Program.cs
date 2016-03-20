using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GeoWcf.Services;

namespace GeoWcf.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));
            hostGeoManager.Open();

            Console.WriteLine("Service is now running. Press [Enter] to exit.");
            Console.ReadLine();
            hostGeoManager.Close();
        }
    }
}
