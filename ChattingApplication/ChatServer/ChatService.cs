using ChatInterface;
using ChatInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChatService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {

        public ConcurrentDictionary<string, ConnectedClient> _connectedClients =
            new ConcurrentDictionary<string, ConnectedClient>();

        public int Login(string userName)
        {
            //is anyone else logged in with my name?
            foreach (var client in _connectedClients)
            {
                if (client.Key.ToLower() == userName.ToLower())
                {
                    //if yes we return 1
                    return 1;
                }

            }
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            ConnectedClient newClient = new ConnectedClient();
            newClient.connection = establishedUserConnection;
            newClient.UserName = userName;

            _connectedClients.TryAdd(userName, newClient);

            return 0;
        }

        public void SendMessageToAll(string message, string userName)
        {
            foreach (var client in _connectedClients)
            {
                if (client.Key.ToLower() != userName.ToLower() )
                {
                    client.Value.connection.GetMessage(message, userName);
                }
            }
        }

        //public void Test(string value)
        //{
        //    Console.WriteLine(value);
        //}
    }
}
