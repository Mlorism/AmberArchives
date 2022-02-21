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

		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}

		[HttpGet("get")]
		public ActionResult<BookDto> Get([FromBody] GetElementDto dto)
		{
			var book = _bookService.GetBook(dto.Id);

			return Ok(book);
		} // Get()

		[HttpGet("getBooks")]
		public ActionResult<BookDto> GetBooks([FromBody] GetElementsDto dto)
		{
			var books = _bookService.GetBooks(dto.Ids);

			return Ok(books);
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
			var result = _bookService.Add(dto);

			return Ok(result);
		} // CreateBook()

	
		[HttpDelete("delete")]
		[Authorize(Roles = "Admin, Moderator")]
		public ActionResult Delete([FromBody] DeleteElementDto dto)
		{
			var result = _bookService.Delete(dto);

			return Ok(result);
		} // Delete()

		[HttpPut("update")]
		[Authorize(Roles = "Admin, Moderator")]
		public ActionResult Update([FromBody] ModifyBookDto dto)
		{		
			var result =_bookService.Update(dto);

			return Ok(result);
		} // Update()
	}
}
