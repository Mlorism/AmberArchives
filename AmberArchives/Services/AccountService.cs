using AmberArchives.Entities;
using AmberArchives.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
    public class AccountService : IAccountService
    {
		private readonly AmberArchivesDbContext _context;
		private readonly IPasswordHasher<User> _passwordHasher;

		public AccountService(IPasswordHasher<User> passwordHasher)
		{
			_passwordHasher = passwordHasher;
		}

		public AccountService(AmberArchivesDbContext context, IPasswordHasher<User> passwordHasher)
		{
            _context = context;
            _passwordHasher = passwordHasher;

        }

        public void RegisterUser(RegisterUserDto dto)
		{             

            var newUser = new User()
            {
                Email = dto.Email,
                Nationality = dto.Nationality,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
		}
    }
}
