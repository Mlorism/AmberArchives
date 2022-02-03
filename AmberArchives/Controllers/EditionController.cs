using AmberArchives.Models;
using AmberArchives.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Controllers
{
    [Route("api/book/{bookId}/edition")]
    [ApiController]
    public class EditionController : ControllerBase
    {
		private readonly IEditionService _editionService;

		public EditionController(IEditionService editionService)
		{
			_editionService = editionService;
		}
		[HttpPost]
		public ActionResult Post([FromRoute] int bookId, [FromBody]CreateEditionDto dto)
		{
			var newEditionId =_editionService.Create(bookId, dto);

			return Created($"api/book/{bookId}/edition/{newEditionId}", null);
		}

		[HttpGet ("{editionId}")]
		public ActionResult Get([FromRoute] int bookId, [FromRoute] int editionId)
		{
			var edition = _editionService.GetById(bookId, editionId);

			return Ok(edition);
		}

		[HttpGet()]
		public ActionResult Get([FromRoute] int bookId)
		{
			var result = _editionService.GetAll(bookId);

			return Ok(result);
		}
	}
}
