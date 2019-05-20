using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using IMDBDegrees.DAL.Actors.Models;

namespace IMDBDegrees.DAL.Actors
{
    public class ActorContext: DbContext
    {
        public ActorContext(DbContextOptions<ActorContext> options): base(options){}

        public DbSet<Person> Persons { get; set; }
    }
}
