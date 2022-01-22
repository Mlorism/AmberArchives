using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
	public class AuthorDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }		
		public DateTime DateOfBirth { get; set; }	
	}
}
