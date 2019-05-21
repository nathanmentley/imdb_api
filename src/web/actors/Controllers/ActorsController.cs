using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Apache.NMS;
using Apache.NMS.Util;

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

            SendMessageToBackgroundProcess();

            return query.Take(100).ToList();
        }

        private void SendMessageToBackgroundProcess() {
            //TODO: this is copid alot. This boiler plate code should be in a common shared class and setup using the same DI setup that the db context goes through.
            Uri connecturi = new Uri("activemq:tcp://activemq:61616");
            
            // NOTE: ensure the nmsprovider-activemq.config file exists in the executable folder.
            IConnectionFactory factory = new NMSConnectionFactory(connecturi);

            using(IConnection connection = factory.CreateConnection())
            using(ISession session = connection.CreateSession())
            {
                IDestination destination = SessionUtil.GetDestination(session, "queue://FOO.BAR");

                // Create a consumer and producer
                using(IMessageProducer producer = session.CreateProducer(destination))
                {
                    // Start the connection so that messages will be processed.
                    connection.Start();
                    producer.DeliveryMode = MsgDeliveryMode.Persistent;
                        
                    // Send a message
                    ITextMessage request = session.CreateTextMessage("Message from actor api service.");

                    producer.Send(request);
                }
            }
        }
    }
}
