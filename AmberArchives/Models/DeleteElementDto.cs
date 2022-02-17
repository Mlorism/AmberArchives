using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class DeleteElementDto : ModUserDto
    {
        [Required]
        [Range(1, double.PositiveInfinity)]
        public int ElementId { get; set; }
    }
}
