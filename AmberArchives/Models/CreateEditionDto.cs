using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class CreateEditionDto
    {   
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }
        public string Publisher { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
