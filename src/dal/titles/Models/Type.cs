using System;
using System.ComponentModel.DataAnnotations;

namespace IMDBDegrees.DAL.Titles.Models
{
    public class Type
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

