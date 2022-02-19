using AmberArchives.Entities;
using AmberArchives.Enums;
using Microsoft.EntityFrameworkCore;
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
				var pendingMigrations = _dbContext.Database.GetPendingMigrations();

				if (pendingMigrations != null && pendingMigrations.Any())
				{
					_dbContext.Database.Migrate();
				}

				if (!_dbContext.UserRoles.Any())
				{
					var roles = GetUserRoles();
					_dbContext.UserRoles.AddRange(roles);
					_dbContext.SaveChanges();
				}

				if (!_dbContext.Shelfs.Any())
				{
					var shelfs = GetShelfs();
					_dbContext.Shelfs.AddRange(shelfs);
					_dbContext.SaveChanges();
				}
			}
		} // Seed()
		private IEnumerable<UserRole> GetUserRoles()
		{
			var userRoles = new List<UserRole>()
			{
				new UserRole()
				{
					RoleType = UserRoleEnum.Admin
				},

				new UserRole()
				{
					RoleType = UserRoleEnum.Moderator
				},

				new UserRole()
				{
					RoleType = UserRoleEnum.User
				},
			};

			return userRoles;
		} // GetUserRoles()

		private IEnumerable<Shelf> GetShelfs()
		{
			var shelfs = new List<Shelf>()
			{
				new Shelf()
				{
					Name = "ToRead"
				},
				new Shelf()
				{
					Name = "CurrentlyReading"
				},
				new Shelf()
				{
					Name = "Read"
				},
			};

			return shelfs;
		} // GetShelfs()







	}
}


