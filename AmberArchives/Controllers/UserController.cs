using AmberArchives.Entities;
using AmberArchives.Models;
using AmberArchives.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Controllers
{
	[Route("api/user")]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly AmberArchivesDbContext _dbContext;
		public UserController(IUserService userService, AmberArchivesDbContext dbContext)
		{
			_userService = userService;
			_dbContext = dbContext;
		}

		[HttpPost("rate")]
		public ActionResult RateBook([FromBody] RateBookDto dto)
		{
			_userService.Rate(dto);

			return Ok();
		} // RateBook()



	}
}
