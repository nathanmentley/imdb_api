using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace IMDBDegrees.DAL.Titles.Models
{
    public class Title
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string PrimaryTitle { get; set; }
        [Required]
        public string OriginalTitle { get; set; }
        [Required]
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        [Required]
        public int Runtime { get; set; }

        public ICollection<Genre> Genres { get; set; }
        public Type Type { get; set; }
    }
}

