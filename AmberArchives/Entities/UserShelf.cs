using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class UserShelf
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ShelfId { get; set; }		
		public int EditionId { get; set; }
		public virtual User User { get; set; }
		public virtual Shelf Shelf { get; set; }		
		public virtual Edition Edition { get; set; }

	}
}
