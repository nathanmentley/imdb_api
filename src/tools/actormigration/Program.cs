using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using IMDBDegrees.DAL.Actors;

namespace IMDBDegrees.Tools.ActorMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<ActorContext> optionsBuilder = new DbContextOptionsBuilder<ActorContext>();
            optionsBuilder.UseSqlServer(@"Server=actors-db;Database=Actors;User=sa;Password=Your_password123");

            using(ActorContext context = new ActorContext(optionsBuilder.Options)) {
                context.Database.Migrate();
            }
        }
    }
}
