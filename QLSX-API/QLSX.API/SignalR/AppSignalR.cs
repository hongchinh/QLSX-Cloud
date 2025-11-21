using Microsoft.AspNetCore.SignalR;
using QLSX.Shared.Models;
using System.Threading.Tasks;

namespace Sale.API.SignalR
{
    public class AppSignalR : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        public async Task SendObject(ThuChiModel message)
        {
            await Clients.All.SendAsync("ReceiveObject", message);
        }
    }
}
