using AmberArchives.Models;
using AmberArchives.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
            _accountService = accountService;

        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
		{
            _accountService.RegisterUser(dto);

            return Ok();
		}

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginDto dto)
		{
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
		}
    }
}
