using AmberArchives.Entities;
using AmberArchives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives
{
    public class AmberArchivesSeeder
    {
		private readonly AmberArchivesDbContext _dbContext;

		public AmberArchivesSeeder(AmberArchivesDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Seed()
		{
			if (_dbContext.Database.CanConnect())
			{
				if (!_dbContext.UserRoles.Any())
				{
					var roles = GetUserRoles();
					_dbContext.UserRoles.AddRange(roles);
					_dbContext.SaveChanges();
				}
			}
}
		private IEnumerable<UserRole> GetUserRoles()
		{
			var userRoles = new List<UserRole>()
			{
				new UserRole()
				{
					RoleType = UserRoleEnum.User
				},

				new UserRole()
				{
					RoleType = UserRoleEnum.Admin
				}
			};

			return userRoles;
		} // GetUserRoles()







	}
}


