using AmberArchives.Entities;
using AmberArchives.Exceptions;
using AmberArchives.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
    public class BookService: IBookService
    {
		private readonly AmberArchivesDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		private readonly IUserContextService _userContextService;
		public BookService(AmberArchivesDbContext dbContext, IMapper mapper, ILogger<BookService> logger, IUserContextService userContextService)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_logger = logger;
			_userContextService = userContextService;
		}

        public BookDto GetBook(int id)
		{
			var book = _dbContext
				.Books
				.Include(b => b.Author)
				.Include(b => b.Editions)
				.FirstOrDefault(b => b.Id == id);

			if (book is null)
			{
				throw new NotFoundException("Book not found");
			}

			var result = _mapper.Map<BookDto>(book);
			return result;
		} // GetBook()

		public PageResult<BookDto> GetAll(BookQuery query)
		{
			var baseQuery = _dbContext
				.Books
				.Include(b => b.Author)
				.Include(b => b.Editions)
				.Where(b => query.SearchPhraze == null || (b.OriginalTitle.ToLower().Contains(query.SearchPhraze.ToLower())));


			if (!string.IsNullOrEmpty(query.SortBy))
			{
				var columnsSelector = new Dictionary<string, Expression<Func<Book, object>>>()
				{
					{nameof(Book.OriginalTitle), b => b.OriginalTitle},
					{nameof(Book.Author.LastName), b => b.Author.LastName},
					{nameof(Book.AverageRating), b => b.AverageRating},
				};

				var selectedColumn = columnsSelector[query.SortBy];

				baseQuery = query.SortDirection == Enums.SortDirection.ASC 
					? baseQuery.OrderBy(selectedColumn) 
					: baseQuery.OrderByDescending(selectedColumn);
			}
			

			var books = baseQuery
				.Skip(query.PageSize * (query.PageNumber - 1))
				.Take(query.PageSize)
				.ToList();

			var booksDto = _mapper.Map<List<BookDto>>(books);

			var result = new PageResult<BookDto>(booksDto, baseQuery.Count(), query.PageSize, query.PageNumber);

			return result;
		} // Get()

		public int Add(CreateBookDto dto)
		{
			// var user = _userContextService.GetUserId; zdobędziemy id usera, który dodał do bazy książkę, trzeba dorzucić datę i zrobić na to miejsce w bazie
			var book = _mapper.Map<Book>(dto);
			_dbContext.Books.Add(book);
			_dbContext.SaveChanges();

			return book.Id;
		} // Add()

		public void Delete(int id)
		{
			_logger.LogWarning($"Book with id {id} DELETE action invoked");

			var book = _dbContext
				.Books
				.FirstOrDefault(b => b.Id == id);

			if (book is null)
			{
				throw new NotFoundException("Book not found");
			}

			_dbContext.Books.Remove(book);
			_dbContext.SaveChanges();

	
		} // Delete()

		public void Update(ModifyBookDto dto)
		{
			_logger.LogInformation($"Book with id {dto.Id} UPDATE action invoked");

			var book = _dbContext
				.Books
				.Include(b => b.Author)
				.FirstOrDefault(b => b.Id == dto.Id);

			if (book is null)
			{
				throw new NotFoundException("Book not found");
			}

			if (!(dto.OriginalReleaseDate is null))
			{
				book.OriginalReleaseDate = dto.OriginalReleaseDate;
			}

			if (!(dto.OriginalTitle is null))
			{
				book.OriginalTitle = dto.OriginalTitle;
			}

			if (!(dto.AuthorId is null))
			{
				book.AuthorId = (int)dto.AuthorId;
			}

			_dbContext.SaveChanges();
		} // Modify()


	}
}
