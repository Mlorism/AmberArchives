﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(50)]
        public string OriginalTitle { get; set; }
        [Required]
        [Range(1, double.PositiveInfinity)]
        public int AuthorId { get; set; }
        public DateTime? OriginalReleaseDate { get; set; }
    }
}