using AmberArchives.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class CreateBookDto : ModUserDto
    {
        [Required]
        [MaxLength(50)]
        public string OriginalTitle { get; set; }
        [Required]
        [Range(1, double.PositiveInfinity)]
        public int AuthorId { get; set; }
        public DateTime? OriginalReleaseDate { get; set; }
        public List<Genere> Generes { get; set; }

    }
}
