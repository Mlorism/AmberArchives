using AmberArchives.Entities;
using AmberArchives.Enums;
using AmberArchives.Models;
using AmberArchives.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
	[ApiController]
	[Authorize]
	public class BookController : ControllerBase
	{
		private readonly IBookService _bookService;
		private readonly AmberArchivesDbContext _dbContext;


		public BookController(IBookService bookService, AmberArchivesDbContext dbContext)
		{
			_bookService = bookService;
			_dbContext = dbContext;
		}

		[HttpGet("get")]
		public ActionResult<BookDto> Get([FromBody] GetElementDto dto)
		{
			var book = _bookService.GetBook(dto.Id);

			return Ok(book);
		} // Get()

		[HttpGet("getAll")]
		public ActionResult<IEnumerable<BookDto>> GetAll([FromBody] BookQuery query)
		{
			var booksDtos = _bookService.GetAll(query);

			return Ok(booksDtos);
		} // GetAll()


		[HttpPost("add")]
		public ActionResult CreateBook([FromBody] CreateBookDto dto)
		{
			if (ControllerHelper.BookDuplicate(_dbContext.Books, dto))
			{
				return BadRequest(ControllerHelper.Messages.bookDuplicate);
			}

			else if (!_dbContext.Authors.Any(a => a.Id == dto.AuthorId))
			{
				return NotFound($"Author {ControllerHelper.Messages.idDontExist}");
			}
			var id = _bookService.Add(dto);

			return Ok();
		} // CreateBook()

		[HttpPost("rate")]
		public ActionResult RateBook([FromBody] RateBookDto dto)
		{
			_bookService.Rate(dto);

			return Ok();
		} // RateBook()

		[HttpDelete("delete")]
		[Authorize(Roles = "Admin")]
		public ActionResult Delete([FromBody] DeleteElementDto dto)
		{
			_bookService.Delete(dto.Id, dto.ModUserId);

			return NoContent();
		} // Delete()

		[HttpPut]
		public ActionResult Update([FromBody] ModifyBookDto dto)
		{
			if (!_dbContext.Books.Any(b => b.Id == dto.Id))
			{

				return NotFound($"Book {ControllerHelper.Messages.idDontExist}");
			}

			else if (!(dto.AuthorId is null) && !_dbContext.Authors.Any(a => a.Id == dto.AuthorId))
			{
				return NotFound($"Author {ControllerHelper.Messages.idDontExist}");
			}

			_bookService.Update(dto);

			return Ok();
		} // Update()
	}
}
