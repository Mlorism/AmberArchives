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

				if (!_dbContext.Generes.Any())
				{
					var generes = GetGeneres();
					_dbContext.Generes.AddRange(generes);
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

		private IEnumerable<Genere> GetGeneres()
		{
			var generes = new List<Genere>()
			{
				new Genere()
				{
					Genre = "Action"
				},
				new Genere()
				{
					Genre = "Adventure"
				},
				new Genere()
				{
					Genre = "Graphic Novel"
				},
				new Genere()
				{
					Genre = "Detective"
				},
				new Genere()
				{
					Genre = "Mystery"
				},
				new Genere()
				{
					Genre = "Fantasy"
				},
				new Genere()
				{
					Genre = "Historical Fiction"
				},
				new Genere()
				{
					Genre = "Horror"
				},
				new Genere()
				{
					Genre = "Fiction"
				},
				new Genere()
				{
					Genre = "Romance"
				},
				new Genere()
				{
					Genre = "Sci-Fi"
				},
				new Genere()
				{
					Genre = "History"
				},
				new Genere()
				{
					Genre = "Military"
				},
					new Genere()
				{
					Genre = "Memoir"
				},
				new Genere()
				{
					Genre = "Poetry"
				},
				new Genere()
				{
					Genre = "Self-Help"
				},
				new Genere()
				{
					Genre = "Biography"
				},
					new Genere()
				{
					Genre = "Dystopian"
				},
				new Genere()
				{
					Genre = "Paranormal"
				},
				new Genere()
				{
					Genre = "Children’s"
				},
					new Genere()
				{
					Genre = "Travel"
				},
						new Genere()
				{
					Genre = "Humor"
				},
			};

			return generes;
		} // GetGeneres()







	}
}


