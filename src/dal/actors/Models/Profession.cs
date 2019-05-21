using System;
using System.ComponentModel.DataAnnotations;

namespace IMDBDegrees.DAL.Actors.Models
{
    public class Profession
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
