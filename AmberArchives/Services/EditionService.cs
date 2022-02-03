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
		}
		public int Create(int bookId, CreateEditionDto dto)
		{
			var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
			if (book is null)
			{
				throw new NotFoundException("Book not found");
			}

			var editionEntity = _mapper.Map<Edition>(dto);

			_context.Editions.Add(editionEntity);
			_context.SaveChanges();

			return editionEntity.Id;

		}

		public EditionDto GetById(int bookId, int editionId)
		{
			var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

			if (book is null)
			{
				throw new NotFoundException("Book not found");
			}

			var edition = _context.Editions.FirstOrDefault(e => e.Id == editionId);
			if (edition is null || edition.BookId != bookId)
			{
				throw new NotFoundException("Edition not found");
			}

			var editionDto = _mapper.Map<EditionDto>(edition);

			return editionDto;
		}

		public List<EditionDto> GetAll(int bookId)
		{
			var book = _context
				.Books
				.Include(b => b.Editions)
				.FirstOrDefault(b => b.Id == bookId);

			if (book is null)
			{
				throw new NotFoundException("Book not found");
			}

			var editionDtos = _mapper.Map<List<EditionDto>>(book.Editions);

			return editionDtos;
		}
	}
}
