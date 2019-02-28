using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TimeMachine.DAL;
using TimeMachine.Models;

namespace TimeMachine.Controllers
{
    [Route("api/[controller]")]
    public class SnapshotController : Controller
    {
        private readonly TimeMachineContext _db;

        public SnapshotController(TimeMachineContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var snapshot = _db.Snapshots.Find(id);

            if (snapshot == null)
            {
                return BadRequest("Snapshot not found.");
            }

            return Content(snapshot.Json, "application/json");
        }

        [HttpGet("all/headers")]
        public IEnumerable<object> GetAllHeaders()
        {
            return from s in _db.Set<SubRedditSnapshot>()
                   orderby s.Timestamp descending
                   select new
                   {
                       id = s.ID,
                       timestamp = s.Timestamp
                   };
        }
    }
}
