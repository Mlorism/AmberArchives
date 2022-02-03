using AmberArchives.Entities;
using AmberArchives.Exceptions;
using AmberArchives.Models;
using AutoMapper;
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
	}
}
