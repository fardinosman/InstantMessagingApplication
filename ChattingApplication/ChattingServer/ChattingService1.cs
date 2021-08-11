
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChattingServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChattingService1" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        
    }
}
