using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class ModUserDto
    {
        [Required]
        public int? ModUserId { get; set; }
    }
}
