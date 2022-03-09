using AmberArchives.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models
{
    public class ModifyUserDto : ModUserDto
    {
        [Required]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public LanguageEnum? PrimaryLanguage { get; set; }
        public int? RoleId { get; set; }
    }
}
