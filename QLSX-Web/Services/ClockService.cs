using CRMApp.Chat;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMApp.Services
{
    public class ClockService
    {
        private readonly IHubContext<ClockHub> _clockHubContext;

        public ClockService(IHubContext<ClockHub> hub)
        {
            _clockHubContext = hub;
        }

        public void Tick()  // Start|Stop
        {
            var time = DateTime.UtcNow.ToString("HH:mm:ss tt zz");
            _clockHubContext.Clients.All.SendAsync("tickUI", time);
        }
    }
}
