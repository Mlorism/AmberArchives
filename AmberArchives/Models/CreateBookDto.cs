using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class CreateBookDto
    {
        public string OriginalTitle { get; set; }
        public int AuthorId { get; set; }
        public DateTime OriginalReleaseDate { get; set; }
    }
}
