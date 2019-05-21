using System;
using System.IO;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using CommandLine;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

using IMDBDegrees.DAL.Actors;
using IMDBDegrees.DAL.Actors.Models;

namespace IMDBDegrees.Tools.ActorMigration
{
    class Program
    {
        public class Options
        {
            [Option('i', "import", Required = false, HelpText = "Import data from csv.")]
            public bool Import { get; set; }
        }


        //nconst	primaryName	birthYear	deathYear	primaryProfession	knownForTitles
        public class ActorRecord
        {
            [Name("nconst")]
            public string Nconst { get; set; }
            [Name("primaryName")]
            public string PrimaryName { get; set; }
            [Name("birthYear")]
            public string BirthYear { get; set; }
            [Name("deathYear")]
            public string DeathYear { get; set; }
            [Name("primaryProfession")]
            public string PrimaryProfession { get; set; }
            [Name("knownForTitles")]
            public string KnownForTitles { get; set; }
        }

        static void Main(string[] args)
        {
            DbContextOptionsBuilder<ActorContext> optionsBuilder = new DbContextOptionsBuilder<ActorContext>();
            optionsBuilder.UseSqlServer(@"Server=actors-db;Database=Actors;User=sa;Password=Your_password123");

            using(ActorContext context = new ActorContext(optionsBuilder.Options)) {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                context.Database.Migrate();

                Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => {
                        if (o.Import) {
                            //import contacts from csv
                            using (StreamReader reader = new StreamReader("/data/name.basics.tsv")) {
                                Configuration config = new Configuration();
                                config.Delimiter = "\t";

                                int counter = 0;
                                using (CsvReader csv = new CsvReader(reader, config)) {
                                    foreach(ActorRecord record in csv.EnumerateRecords(new ActorRecord())) {
                                        Person person = new Person();

                                        //parse csv data
                                        int birthYear = 0;
                                        Int32.TryParse(record.BirthYear, out birthYear);
                                        int deathYear = -1;
                                        Int32.TryParse(record.DeathYear, out deathYear);

                                        if(!context.Persons.Any(x => x.Nconst == record.Nconst)) {
                                            //map csv to models.
                                            person.Nconst = record.Nconst;
                                            person.Name = record.PrimaryName;
                                            person.BirthYear = birthYear;
                                            person.DeathYear = deathYear == -1 ? null: (int?)deathYear;

                                            context.Add(person);

                                            //only save after 1000 entries to speed things up a bit.
                                            if(counter % 1000 == 0) {
                                                context.SaveChanges();
                                                counter = 0;
                                            }
                                            counter += 1;
                                        }
                                    }
                                }
                            }

                            //save anything that hasn't been saved yet.
                            context.SaveChanges();
                        }
                    });
            }
        }
    }
}
