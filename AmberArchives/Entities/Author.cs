using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class Author : ModUserEntity
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Column(TypeName = "date")]
		public DateTime DateOfBirth { get; set; }
		public virtual List<Book> Books { get; set; }
	}
}
