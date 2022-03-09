using AmberArchives.Entities;
using AmberArchives.Enums;
using AmberArchives.Exceptions;
using AmberArchives.Models;
using AutoMapper;
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
		private readonly IMapper _mapper;

		public AccountService(IPasswordHasher<User> passwordHasher)
		{
			_passwordHasher = passwordHasher;
		}

		public AccountService(AmberArchivesDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticatonSettings, IMapper mapper)
		{
			_context = context;
			_passwordHasher = passwordHasher;
			_authenticatonSettings = authenticatonSettings;
			_mapper = mapper;


		}
		public bool Register(RegisterUserDto dto)
		{
			var newUser = _mapper.Map<User>(dto);

			newUser.RoleId = 3;

			var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

			newUser.PasswordHash = hashedPassword;
			_context.Users.Add(newUser);
			_context.SaveChanges();

			return true;
		} // RegisterUser()

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
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRoleEnum), user.Role.RoleType)),
				new Claim("PrimaryLanguage", Enum.GetName(typeof(LanguageEnum), user.PrimaryLanguage)),
				new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-mm-dd")),
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
		} // GenerateJwt()

		public bool Update(ModifyUserDto dto)
		{
			return true;
		} // ModifyUser()
	}
}
