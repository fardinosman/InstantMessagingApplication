﻿using ChatInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatInterface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChatService" in both code and config file together.
    [ServiceContract(CallbackContract =typeof(IClient))]
    public interface IChatService
    {
        [OperationContract]
         int Login(string userName);
        [OperationContract]
         void SendMessageToAll(string message, string userName);

        [OperationContract]
        void Logout();

        [OperationContract]
        List<string> GetCurrentUsers();




        //[OperationContract]
        //void Test(string value);
    }
}
