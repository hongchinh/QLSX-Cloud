using CRMApp.Data;
using CRMApp.Extensions;
using CRMApp.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CRMApp.Chat
{
    public class WeatherHub : Hub
    {
        private readonly WeatherBackgroundService _weatherBackgroundService;
        public WeatherHub(WeatherBackgroundService weatherBackgroundService)
        {
            _weatherBackgroundService = weatherBackgroundService;
        }

        public ChannelReader<NotificationModel> StreamWeather()
        {
            return _weatherBackgroundService.StreamWeather().AsChannelReader(10);
        }
    }
}
