using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using TimeMachine.DAL;

namespace TimeMachine.Services
{
    public class ScheduledService
    {
        private readonly TimeMachineContext _db;
        private readonly HttpClient _http;
        private readonly string datasource;

        public ScheduledService
        (
            TimeMachineContext db,
            HttpClient http,
            IConfiguration config
        )
        {
            _db = db;
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

        public void RunTask()
        {
            // TODO: Convert to async. Save to database here.
        }

    }
}