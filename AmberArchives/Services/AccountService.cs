using AmberArchives.Entities;
using AmberArchives.Enums;
using AmberArchives.Exceptions;
using AmberArchives.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
		private readonly AmberArchivesDbContext _dbContext;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly AuthenticationSettings _authenticatonSettings;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public AccountService(AmberArchivesDbContext dbContext, IPasswordHasher<User> passwordHasher, ILogger<BookService> logger)
		{
			_dbContext = dbContext;
			_passwordHasher = passwordHasher;
			_logger = logger;
		}

		public AccountService(AmberArchivesDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticatonSettings, IMapper mapper)
		{
			_dbContext = context;
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
			_dbContext.Users.Add(newUser);
			_dbContext.SaveChanges();

			return true;
		} // RegisterUser()

		public string GenerateJwt(LoginDto dto)
		{
			var user = _dbContext.Users
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
			_logger.LogInformation($"User with id {dto.Id} UPDATE action infoked by user {dto.ModUserId}");

			var user = _dbContext
				.Users
				.Include(u => u.Role)
				.FirstOrDefault(u => u.Id == dto.Id);
			var modUser = _dbContext
				.Users
				.Include(u => u.Role)
				.FirstOrDefault(u => u.Id == dto.ModUserId);

			if (modUser is null)			{
				_logger.LogInformation($"User with id {dto.ModUserId} does not exist. UPDATE action cancelled.");
				throw new NotFoundException("User invoking UPDATE action does not exist");
			}

			if (user is null)
			{
				_logger.LogInformation($"User with id {dto.Id} does not exist. UPDATE action cancelled.");
				throw new NotFoundException("User not found");
			}

			if (dto.Email != "")
			{
				var isEmailInUse = _dbContext.Users.Any(u => u.Email == dto.Email);

				if (!isEmailInUse)
				{
					user.Email = dto.Email;

				}
				else
				{
					_logger.LogInformation($"Email {dto.Email} is connected to another account. UPDATE action cancelled.");
					throw new ValueAlreadyExistException("Email in use by other user");
				}
			}

			if (dto.Username != "")
			{
				var usernameInUse = _dbContext.Users.Any(u => u.Username == dto.Username);

				if (!usernameInUse)
				{
					user.Username = dto.Username;
				}
				else
				{
					_logger.LogInformation($"Username {dto.Username} is connected to another account. UPDATE action cancelled.");
					throw new ValueAlreadyExistException("Username in use by other user");
				}
			}

			if (dto.DateOfBirth is not null)
			{
				user.DateOfBirth = dto.DateOfBirth;
			}

			if (dto.PrimaryLanguage is not null)
			{
				user.PrimaryLanguage = (LanguageEnum)dto.PrimaryLanguage;
			}

			if (dto.RoleId is not null)
			{
				if (modUser.Role.RoleType == UserRoleEnum.Admin)
				{
					user.RoleId = (int)dto.RoleId;
				}
				else
				{
					_logger.LogInformation($"User's ({dto.ModUserId}) role does not allow updating other users role. UPDATE action cancelled.");
					throw new AccessDeniedException("Changing role not permitted for user.");
				}
			}

			_dbContext.SaveChanges();

			return true;
		} // ModifyUser()
	}
}
