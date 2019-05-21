using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace IMDBDegrees.DAL.Actors.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nconst { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int BirthYear { get; set; }
        public int? DeathYear { get; set; }

        public ICollection<Profession> Professions { get; set; }
    }
}
