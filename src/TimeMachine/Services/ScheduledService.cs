using System;
using System.Net.Http;

namespace TimeMachine.Services
{
    public class ScheduledService
    {
        private readonly HttpClient _http;

        // TODO: Inject Data Access Layer object
        public ScheduledService(HttpClient http)
        {
            _http = http;

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