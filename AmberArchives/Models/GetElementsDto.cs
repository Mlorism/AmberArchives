using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class GetElementsDto
    {
        [Required]        
        public int[] Ids { get; set; }
    }
}
