using System;
using System.ComponentModel.DataAnnotations;

namespace IMDBDegrees.DAL.Actors.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int BirthYear { get; set; }
        public int? DeathYear { get; set; }
    }
}
