using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IMDBDegrees.DAL.Actors
{
    public class ActorContextFactory : IDesignTimeDbContextFactory<ActorContext>
    {
        public ActorContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ActorContext>();
            optionsBuilder.UseSqlServer(@"Server=actors-db;Database=Actors;User=sa;Password=Your_password123");

            return new ActorContext(optionsBuilder.Options);
        }
    }
}
