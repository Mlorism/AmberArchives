using AmberArchives.Entities;
using AmberArchives.Models;
using AmberArchives.Services;
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
		private readonly IBookService _bookService;
		private readonly AmberArchivesDbContext _dbContext;

		public BookController(IBookService bookService, AmberArchivesDbContext dbContext)
		{
			_bookService = bookService;
			_dbContext = dbContext;

		}

		[HttpGet]
		public ActionResult<IEnumerable<BookDto>> GetAll()
		{
			var booksDtos = _bookService.Get();

			return Ok(booksDtos);
		} // GetAll()

		[HttpGet("{id}")]
		public ActionResult<BookDto> Get([FromRoute] int id)
		{
			if (id < 1)
			{
				return BadRequest(ControllerHelper.Messages.idToSmall);
			}
			var book = _bookService.GetBook(id);

			if (book is null)
			{
				return NotFound();
			}
			
			return Ok(book);
		} // Get()

		[HttpPost]
		public ActionResult CreateBook([FromBody] CreateBookDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			else if (ControllerHelper.BookDuplicate(_dbContext.Books, dto))
			{
				return BadRequest(ControllerHelper.Messages.bookDuplicate);
			}

			else if (!_dbContext.Authors.Any(a => a.Id == dto.AuthorId))
			{
				return NotFound($"Author {ControllerHelper.Messages.idDontExist}");
			}

			var id = _bookService.Add(dto);

			return Created($"api/book/{id}", null);
		} // CreateBook()

		[HttpDelete("{id}")]
		public ActionResult Delete([FromRoute] int id)
		{
			var result = _bookService.Delete(id);

			if (result == false)
			{
				return NotFound();
			}

			return Ok(id);
		} // Delete()

		[HttpPut]
		public ActionResult Update([FromBody] ModifyBookDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}			

			else if (!_dbContext.Books.Any(b => b.Id == dto.Id))
			{
				
				return NotFound($"Book {ControllerHelper.Messages.idDontExist}");
			}

			else if (!(dto.AuthorId is null) && !_dbContext.Authors.Any(a => a.Id == dto.AuthorId))
			{
				return NotFound($"Author {ControllerHelper.Messages.idDontExist}");
			}

			var result = _bookService.Update(dto);

			if (result)
			{
				return Ok(result);
			}

			else return BadRequest();
		} // Update()
	}
}
