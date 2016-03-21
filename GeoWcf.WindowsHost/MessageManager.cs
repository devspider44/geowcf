using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWcf.WindowsHost.Contract;

namespace GeoWcf.WindowsHost
{
    public class MessageManager :IMessageService
    {
        public void ShowMessage(string message)
        {
            MainWindow.MainUI.Show(message);
        }
    }
}
