using AmberArchives.Entities;
using AmberArchives.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		private readonly IMapper _mapper;

		public BookController(AmberArchivesDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;

		}

		[HttpGet]
		public ActionResult<IEnumerable<BookDto>> GetAll()
		{
			var books = _dbContext
				.Books
				.Include(b => b.Author)
				.Include(b => b.Editions)
				.ToList();
			var booksDto = _mapper.Map<List<BookDto>>(books);

			return Ok(booksDto);
		}

		[HttpGet("{id}")]
		public ActionResult<BookDto> Get([FromRoute] int id)
		{
			var book = _dbContext
				.Books
				.Include(b => b.Author)
				.Include(b => b.Editions)
				.FirstOrDefault(b => b.Id == id);

			if (book is null)
			{
				return NotFound();
			}

			var bookDto = _mapper.Map<BookDto>(book);
			return Ok(bookDto);
		}

		[HttpPost]
		public ActionResult CreateBook([FromBody] CreateBookDto dto)
		{
			var book = _mapper.Map<Book>(dto);
			_dbContext.Books.Add(book);
			_dbContext.SaveChanges();

			return Created($"api/book/{book.Id}", null);
		}


	}
}
