using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using GeoWcf.Contracts;
using GeoWcf.Services;

namespace GeoWcf.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));

            //string address = "net.tcp://localhost:8009/GeoService";
            //Binding binding = new NetTcpBinding();
            //Type contract = typeof (IGeoService);

            //hostGeoManager.AddServiceEndpoint(contract, binding, address);

            //string address2 = "http://localhost/GeoService";
            //Binding binding2 = new BasicHttpBinding();

            //hostGeoManager.AddServiceEndpoint(contract, binding2, address2);

            hostGeoManager.Open();

            //ServiceHost hostStatefulGeoManager = new ServiceHost(typeof(StatefulGeoManager));
            //hostStatefulGeoManager.Open();

            Console.WriteLine("Service is now running. Press [Enter] to exit.");
            Console.ReadLine();
            hostGeoManager.Close();
           // hostStatefulGeoManager.Close();
        }
    }
}
