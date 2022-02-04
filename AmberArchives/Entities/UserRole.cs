using AmberArchives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class UserRole
    {
		public int Id { get; set; }
		public UserRoleEnum RoleType { get; set; }
	}
}
