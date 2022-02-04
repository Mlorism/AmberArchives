using AmberArchives.Entities;
using AmberArchives.Exceptions;
using AmberArchives.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
	public class EditionService : IEditionService
	{
		private readonly AmberArchivesDbContext _context;
		private readonly IMapper _mapper;

		public EditionService(AmberArchivesDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		} // EditionService()
		public int Create(int bookId, CreateEditionDto dto)
		{
			var book = GetEditionById(bookId);				

			var editionEntity = _mapper.Map<Edition>(dto);

			_context.Editions.Add(editionEntity);
			_context.SaveChanges();

			return editionEntity.Id;

		} // Create()

		public EditionDto GetById(int bookId, int editionId)
		{
			var book = GetEditionById(bookId);
						
			var edition = _context.Editions.FirstOrDefault(e => e.Id == editionId);
			if (edition is null || edition.BookId != bookId)
			{
				throw new NotFoundException("Edition not found");
			}

			var editionDto = _mapper.Map<EditionDto>(edition);

			return editionDto;
		} // GetById()

		public List<EditionDto> GetAll(int bookId)
		{
			var book = GetEditionById(bookId);				

			var editionDtos = _mapper.Map<List<EditionDto>>(book.Editions);

			return editionDtos;
		} // GetAll()

		public void RemoveAll(int bookId)
		{
			var book = GetEditionById(bookId);

			_context.RemoveRange(book.Editions);
			_context.SaveChanges();
		} // RemoveAll()

		private Book GetEditionById(int bookId)
		{
			var book = _context
				.Books
				.Include(b => b.Editions)
				.FirstOrDefault(b => b.Id == bookId);

			if (book is null)
			{
				throw new NotFoundException("Book not found");
			}

			return book;
		} // GetEditionById()
	}
}
