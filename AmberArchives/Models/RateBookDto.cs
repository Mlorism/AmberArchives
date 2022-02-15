using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class RateBookDto : ModUserDto
    {
		public int BookId { get; set; }
		public int Rating { get; set; }
	}
}
