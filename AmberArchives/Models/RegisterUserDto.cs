using AmberArchives.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class RegisterUserDto
    {		
		public string Email { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public LanguageEnum PrimaryLanguage { get; set; }
		public DateTime? DateOfBirth { get; set; }		
	}
}
