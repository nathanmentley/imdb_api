using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult<IEnumerable<Person>> Get(string name, int? birthYear)
        {
            //Build query and get result.
            // normally, I'd have a layer between controllers and the dal, but for this example that's a bit much.
            // it's useful to move business logic there, DI it, and unit test it.
            IQueryable<Person> query = db.Persons;

            if(birthYear.HasValue) {
                query = query.Where(x => x.BirthYear == birthYear.Value);
            }
            if(name != null) {
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{name}%"));
            }

            return query.Take(100).ToList();
        }
    }
}
