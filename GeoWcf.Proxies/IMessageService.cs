﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoWcf.Client.Contracts
{
    [ServiceContract]
    public interface IMessageService
    {
     [OperationContract]
        void ShowMessage(string message);
    }
}
