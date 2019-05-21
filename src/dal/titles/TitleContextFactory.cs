using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IMDBDegrees.DAL.Titles
{
    public class TitleContextFtitley : IDesignTimeDbContextFactory<TitleContext>
    {
        public TitleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TitleContext>();
            optionsBuilder.UseSqlServer(@"Server=titles-db;Database=Titles;User=sa;Password=Your_password123");

            return new TitleContext(optionsBuilder.Options);
        }
    }
}
