using AmberArchives.Entities;
using AmberArchives.Exceptions;
using AmberArchives.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
	public class AccountService : IAccountService
	{
		private readonly AmberArchivesDbContext _context;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly AuthenticationSettings _authenticatonSettings;

		public AccountService(IPasswordHasher<User> passwordHasher)
		{
			_passwordHasher = passwordHasher;
		}

		public AccountService(AmberArchivesDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticatonSettings)
		{
			_context = context;
			_passwordHasher = passwordHasher;
			_authenticatonSettings = authenticatonSettings;


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

		public string GenerateJwt(LoginDto dto)
		{
			var user = _context.Users
				.Include(x => x.Role)
				.FirstOrDefault(x => x.Email == dto.Email);

			if (user is null)
			{
				throw new BadRequestException("Invalid user name or password");
			}

			var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

			if (result == PasswordVerificationResult.Failed)
			{
				throw new BadRequestException("Invalid user name or password");
			}

			var claims = new List<Claim>(){
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
				new Claim(ClaimTypes.Role, $"{user.Role.RoleType}"),
				new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-mm-dd")),
				new Claim("Nationality", user.Nationality),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticatonSettings.JwtKey));
			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(_authenticatonSettings.JwtExpireDays);

			var token = new JwtSecurityToken(_authenticatonSettings.JwtIssuer,
				_authenticatonSettings.JwtIssuer,
				claims,
				expires: expires,
				signingCredentials: cred);

			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(token);
		}
	}
}
