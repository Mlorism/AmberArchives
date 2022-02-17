using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class ModifyBookDto : ModUserDto
    {
        [Required]
        [Range(1, double.PositiveInfinity)]
        public int BookId { get; set; }
        [MaxLength(50)]
        public string OriginalTitle { get; set; }
        [Range(1, double.PositiveInfinity)]
        public int? AuthorId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? OriginalReleaseDate { get; set; }
    }
}
