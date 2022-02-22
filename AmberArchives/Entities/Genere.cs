using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class Genere
    {
		public int Id { get; set; }
		public string Genre { get; set; }
		public virtual List<Book> Books { get; set; }
	}
}
