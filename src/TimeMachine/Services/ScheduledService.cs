using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeMachine.DAL;
using TimeMachine.Models;

namespace TimeMachine.Services
{
    public class ScheduledService
    {
        private readonly IServiceScopeFactory _srvFactory;
        private readonly HttpClient _http;
        private readonly string datasource;

        public ScheduledService
        (
            IServiceScopeFactory srvFactory,
            HttpClient http,
            IConfiguration config
        )
        {
            _srvFactory = srvFactory;
            _http = http;
            datasource = config.GetValue<string>("SubReddit");

            // 1 second interval precision
            var timer = new System.Timers.Timer(1000);
            
            int lastHour = DateTime.Now.Hour;
            timer.Elapsed += new System.Timers.ElapsedEventHandler((a, b) =>
            {
                if (lastHour < DateTime.Now.Hour
                || (lastHour == 23 && DateTime.Now.Hour == 0))
                {
                    lastHour = DateTime.Now.Hour;
                    RunTask();
                }
            });

            timer.Enabled = true;
        }

        public async void RunTask()
        {
            var res           = await _http.GetAsync(datasource);
            var subredditJson = await res.Content.ReadAsStringAsync();
            
            // Create and run a scoped task with dbContext.
            using (var scope = _srvFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TimeMachineContext>();
                db.Snapshots.Add(new SubRedditSnapshot
                {
                    Timestamp = DateTime.Now,
                    Json = subredditJson
                });
                db.SaveChanges();
            }
        }

    }
}