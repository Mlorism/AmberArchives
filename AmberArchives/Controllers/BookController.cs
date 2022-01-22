using AmberArchives.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Controllers
{
	[Route("api/book")]
	public class BookController : ControllerBase
	{
		private readonly AmberArchivesDbContext _dbContext;

		public BookController(AmberArchivesDbContext dbContext)
		{
			_dbContext = dbContext;

		}

		[HttpGet]
		public ActionResult<IEnumerable<Book>> GetAll()
		{
			var books = _dbContext.Books.ToList();

			return Ok(books);
		}

		[HttpGet("{id}")]
		public ActionResult<Book> Get([FromRoute] int id)
		{
			var book = _dbContext.Books.FirstOrDefault(b => b.Id == id);

			if (book is null)
			{
				return NotFound();
			}
			else
			{
				return Ok(book);
			}
		}



	}
}
