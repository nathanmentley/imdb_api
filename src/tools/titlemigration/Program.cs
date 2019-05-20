using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using IMDBDegrees.DAL.Titles;

namespace IMDBDegrees.Tools.TitleMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<TitleContext> optionsBuilder = new DbContextOptionsBuilder<TitleContext>();
            optionsBuilder.UseSqlServer(@"Server=titles-db;Database=Titles;User=sa;Password=Your_password123");

            using(TitleContext context = new TitleContext(optionsBuilder.Options)) {
                context.Database.Migrate();
            }
        }
    }
}
