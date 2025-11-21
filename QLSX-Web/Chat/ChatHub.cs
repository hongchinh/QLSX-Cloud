using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMApp.Chat
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(string id, string sender, string receiver, string msgTitle, string msgBody)
        {
            await Clients.Client(id).SendAsync("ReceiveMessage", sender, receiver, msgTitle, msgBody);

            Console.WriteLine("ReceiveMessage - sender: " + sender + " receiver:" + receiver + " msgTitle:" + msgTitle + " msgBody:" + msgBody);
        }


    }
}
