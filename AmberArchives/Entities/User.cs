using AmberArchives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class User
    {
		public int Id { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }		
		public DateTime? DateOfBirth { get; set; }		
		public LanguageEnum PrimaryLanguage { get; set; }
		public string PasswordHash { get; set; }
		public int RoleId { get; set; }
		public virtual UserRole Role { get; set; }
	}
}
