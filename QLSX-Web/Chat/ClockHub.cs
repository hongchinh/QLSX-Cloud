using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CRMApp.Chat
{
    public class ClockHub : Hub
    {
        private static bool _clockRunning = false;

        public void Start()
        {
            _clockRunning = true;
            Clients.All.SendAsync("clockStarted");
        }

        public void Stop()
        {
            _clockRunning = false;
            Clients.All.SendAsync("clockStopped");
        }

        public ChannelReader<string> Tick()
        {
            var channel = Channel.CreateUnbounded<string>();
            Task.Run(async () =>
            {
                while (_clockRunning)
                {
                    var time = DateTime.UtcNow.ToString("HH:mm:ss");
                    await channel.Writer.WriteAsync(time);
                    await Task.Delay(1000);
                }
                channel.Writer.TryComplete();
            });
            return channel.Reader;
        }
    }
}
