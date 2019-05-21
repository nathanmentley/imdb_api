using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using IMDBDegrees.DAL.Actors;
using IMDBDegrees.DAL.Actors.Models;

namespace IMDBDegrees.Web.Actors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        protected ActorContext db { get; private set; }
        public ActorsController(ActorContext context) {
            db = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get(int birthYear)
        {
            return db.Persons.Where(x => x.BirthYear == birthYear).ToList();
        }
    }
}
