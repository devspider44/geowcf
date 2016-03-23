using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeoWcf.Contracts;
using GeoWcf.Services;
using GeoWcf.WindowsHost.Contracts;

namespace GeoWcf.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            MainUI = this;

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId;

            _SyncContext = SynchronizationContext.Current;
        }

        private ServiceHost _HostGeoManager = null;
        private ServiceHost _HostMessageManager = null;

        private SynchronizationContext _SyncContext = null;

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager = new ServiceHost(typeof(GeoManager));
            //string address = "net.tcp://localhost:8009/GeoService";
            //System.ServiceModel.Channels.Binding binding = new NetTcpBinding();
            //Type contract = typeof(IGeoService);

            //_HostGeoManager.AddServiceEndpoint(contract, binding, address);
            _HostMessageManager = new ServiceHost(typeof(MessageManager));
            _HostMessageManager.Open();
            _HostGeoManager.Open();
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager.Close();
            _HostMessageManager.Close();
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;


            SendOrPostCallback callback = (arg =>
            {
                lblMessage.Content = message + Environment.NewLine + " (marshalled from thread: " + threadId.ToString() 
                    + " to thread " + Thread.CurrentThread.ManagedThreadId.ToString()             
                    + " | Process " + Process.GetCurrentProcess().Id.ToString();
            });

            _SyncContext.Send(callback, null);
        }

        private void btnInProcSvc_Click(object sender, RoutedEventArgs e)
        {

            Thread thread = new Thread(() =>
            {
                ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");

                IMessageService proxy = factory.CreateChannel();

                proxy.ShowMessage(DateTime.Now.ToLongDateString() + " from in-process call");

                factory.Close();
            });

            thread.IsBackground = true;
            thread.Start();

        }
    }
}
