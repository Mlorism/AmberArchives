using AmberArchives.Entities;
using AmberArchives.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Controllers
{
    [Route("api/shelf")]
    [ApiController]
    [Authorize]
    public class ShelfController : ControllerBase
    {
		private readonly IShelfService _shelfService;

		public ShelfController(IShelfService shelfService)
		{
            _shelfService = shelfService;
        }

        [HttpGet("get")]
        public ActionResult<IEnumerable<Shelf>> Get()
		{
            var shelfs = _shelfService.GetShelfs();

            return Ok(shelfs);
		}
    }
}
