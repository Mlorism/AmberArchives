using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
	public class Book : ModUserEntity
	{	
		public int Id { get; set; }
		public string OriginalTitle { get; set; }
		public int AuthorId { get; set; }
		[Column(TypeName = "date")]
		public DateTime? OriginalReleaseDate { get; set; }
		public int AverageRating { get; set; }
		public virtual Author Author { get; set; }
		public virtual List<Edition> Editions { get; set; }
		public virtual List<BookRating> Ratings { get; set; }
		public virtual List<Genere> Generes { get; set; }
	}
}
