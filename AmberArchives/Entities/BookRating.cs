using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class BookRating
    {
		public int Id { get; set; }
		public int Rating  { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public virtual Book Book { get; set; }
		public virtual User User { get; set; }

	}
}
