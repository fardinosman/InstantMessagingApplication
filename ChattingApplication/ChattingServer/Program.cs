﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChattingServer
{
    class Program
    {
        public static ChattingService1 _server;
        static void Main(string[] args)
        {
            _server = new ChattingService1();
            using (ServiceHost host = new ServiceHost(_server))
            {
                host.Open();
                Console.WriteLine("Server is running");
                Console.ReadLine();
            }
        }
    }
}
