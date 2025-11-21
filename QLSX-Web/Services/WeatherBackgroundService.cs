using Blazored.LocalStorage;

using QLSX.Web.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using QLSX.Shared.Models;

namespace QLSX.Web.Services
{
    public class WeatherBackgroundService : BackgroundService
    {
        private readonly Subject<NotificationModel> _subject = new Subject<NotificationModel>();
        private readonly Random _random = new Random();
        private readonly IMemoryCache _memoryCache;
        public INotificationService<Noti> _notiService;
        public IConfiguration Configuration { get; }
        public WeatherBackgroundService(IConfiguration configuration, INotificationService<Noti> notiService, IMemoryCache memoryCache)
        {
            Configuration = configuration;
            _notiService = notiService;
            _memoryCache = memoryCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string key = "_Key_Token";
                var token = _memoryCache.Get(key);
                var user = (UserModel)_memoryCache.Get("_User_Login");
                if (token != null && user != null)
                {
                    lstCount = await _notiService.GetCountByUserIdAsync();
                }
                if (user == null) lstCount = 0;
                if (user != null) lstCount = user.Id;
                _subject.OnNext(
                                new NotificationModel { Count = lstCount }
                            );
                await Task.Delay(1000*1000);

            }
        }

        public IObservable<NotificationModel> StreamWeather()
        {
            return _subject;
        }
        int lstCount = 0;
      
    }
}
