using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
	public class Book
	{	
		public int Id { get; set; }
		public string OriginalTitle { get; set; }
		public int AuthorId { get; set; }
		[Column(TypeName = "date")]
		public DateTime? OriginalReleaseDate { get; set; }		
		public float AverageRating { get; set; }
		public virtual Author Author { get; set; }
		public virtual List<Edition> Editions { get; set; }
	}
}
