using Microsoft.EntityFrameworkCore;
using TimeMachine.Models;

namespace TimeMachine.DAL
{
    public class TimeMachineContext : DbContext
    {
        public TimeMachineContext(DbContextOptions<TimeMachineContext> options)
            : base(options)
        {
        }

        public DbSet<SubRedditSnapshot> Snapshots { get; set; }
    }
}
