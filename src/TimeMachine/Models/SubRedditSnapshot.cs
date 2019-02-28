using System;

namespace TimeMachine.Models
{
    public class SubRedditSnapshot
    {
        public int ID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Json { get; set; }
    }
}
