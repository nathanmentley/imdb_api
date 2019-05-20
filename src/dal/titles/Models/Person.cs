using System;

namespace IMDBDegrees.DAL.Titles.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int DeathYear { get; set; }
    }
}
