using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeoWcf.Client.Contracts;
using GeoWcf.Contracts;
using GeoWcf.Proxies;

namespace GeoWcf.Client.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GeoClient _Proxy = null;
        public MainWindow()
        {
            InitializeComponent();
            //EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
            //Binding binding = new NetTcpBinding();
            //binding.SendTimeout = new TimeSpan(0, 0, 0, 5);
            //((NetTcpBinding)binding).MaxReceivedMessageSize = 2000000;
            //binding.ReceiveTimeout = new TimeSpan(0,0,0,5);
            
            //_Proxy = new GeoClient(binding,address);
            _Proxy = new GeoClient("netTcp");
        }



        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text != "")
            {
                GeoClient proxy = new GeoClient("netTcp");

                ZipCodeData data = proxy.GetZipinfo(txtZipCode.Text);

                if (data != null)
                {
                    lblCity.Content = data.City;
                    lblState.Content = data.State;
                }

                proxy.Close();
            }
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (txtState.Text != null)
            {
                //EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                //Binding binding = new NetTcpBinding();
                //binding.SendTimeout = new TimeSpan(0,0,0,5);
                //((NetTcpBinding) binding).MaxReceivedMessageSize = 2000000;

                //GeoClient proxy = new GeoClient(binding,address);

                

                IEnumerable<ZipCodeData> data = _Proxy.GetZips(txtState.Text);
                if (data != null)
                {
                    lbxResponse.ItemsSource = data;
                }
                //proxy.Close();
            }
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
            IMessageService proxy = factory.CreateChannel();

            proxy.ShowMessage(txtTextToShow.Text);
            
            factory.Close();

        }
    }
}
