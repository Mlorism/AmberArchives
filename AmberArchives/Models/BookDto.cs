using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class BookDto
    {
		public int Id { get; set; }		
		public string OriginalTitle { get; set; }
		public DateTime? OriginalReleaseDate { get; set; }
		public float AverageRating { get; set; }
		public List<EditionDto> Editions { get; set; }
		public AuthorDto Author { get; set; }
	}
}
