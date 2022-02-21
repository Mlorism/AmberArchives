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
	
		public BookService(AmberArchivesDbContext dbContext, IMapper mapper, ILogger<BookService> logger)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_logger = logger;
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

		public List<BookDto> GetBooks(int[] ids)
		{
			var book = _dbContext
				.Books
				.Include(b => b.Author)
				.Include(b => b.Editions)
				.Where(b => ids.Contains(b.Id));

			if (book is null)
			{
				throw new NotFoundException("No books found");
			}

			var result = _mapper.Map<List<BookDto>>(book);
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

				baseQuery = query.SortDirection == Enums.SortDirectionEnum.ASC 
					? baseQuery.OrderBy(selectedColumn) 
					: baseQuery.OrderByDescending(selectedColumn);
			}

			List<Book> books;

			if (query?.PageSize is null)
			{
				books = baseQuery.ToList();
			}
			else
			{
				books = baseQuery
				.Skip((int)(query.PageSize * (query.PageNumber - 1)))
				.Take((int)query.PageSize)
				.ToList();
			}

			var booksDto = _mapper.Map<List<BookDto>>(books);

			var result = new PageResult<BookDto>(booksDto, baseQuery.Count(), (int)(query?.PageSize is null ? baseQuery.Count() : query.PageSize), (int)(query?.PageNumber is null ? 1 : query.PageNumber));

			return result;
		} // Get()

		public bool Add(CreateBookDto dto)
		{
			_logger.LogInformation($"Book {dto.OriginalTitle} ADD action invoked by user {dto.ModUserId}.");

			var duplicateBook = _dbContext.Books.Any(b => b.OriginalTitle == dto.OriginalTitle && b.AuthorId == dto.AuthorId);
			var author = _dbContext.Authors.Any(a => a.Id == dto.AuthorId);
			var user = _dbContext.Users.FirstOrDefault(u => u.Id == dto.ModUserId);

			if (user is null)
			{
				_logger.LogInformation($"User with id {dto.ModUserId} does not exist. ADD action cancelled.");
				throw new NotFoundException("User does not exist");
			}

			if (duplicateBook)
			{
				_logger.LogInformation($"Book {dto.OriginalTitle} alredy exist. ADD action cancelled.");
				throw new DuplicateException("Book alredy exist");
			}
			else if (!author)
			{
				_logger.LogInformation($"Author with id {dto.AuthorId} does not exist. ADD action cancelled.");
				throw new NotFoundException("Author with given id does not exist");
			}

			var book = _mapper.Map<Book>(dto);
			_dbContext.Books.Add(book);
			_dbContext.SaveChanges();

			_logger.LogInformation($"Book {dto.OriginalTitle} added to DB by user {dto.ModUserId}.");

			return true;
		} // Add()

		public bool Delete(DeleteElementDto dto)
		{
			_logger.LogInformation($"Book with id {dto.ElementId} DELETE action invoked by user {dto.ModUserId}");

			var book = _dbContext
				.Books
				.FirstOrDefault(b => b.Id == dto.ElementId);

			var user = _dbContext.Users.Any(u => u.Id == dto.ModUserId);

			if (!user)
			{
				_logger.LogInformation($"User with id {dto.ModUserId} does not exist. DELETE action cancelled.");
				throw new NotFoundException("User does not exist");
			}

			if (book is null)
			{
				_logger.LogInformation($"Book with id {dto.ElementId} does not exist. DELETE action cancelled.");
				throw new NotFoundException("Book not found");
			}
			_dbContext.Books.Remove(book);
			_dbContext.SaveChanges();
			
			_logger.LogInformation($"Book with id {dto.ElementId} successfully removed by user {dto.ModUserId}");

			return true;
		} // Delete()

		public bool Update(ModifyBookDto dto)
		{
			_logger.LogInformation($"Book with id {dto.BookId} UPDATE action invoked by user {dto.ModUserId}");

			var book = _dbContext
				.Books
				.Include(b => b.Author)
				.FirstOrDefault(b => b.Id == dto.BookId);

			var user = _dbContext.Users.Any(u => u.Id == dto.ModUserId);
			var author = _dbContext.Authors.Any(a => a.Id == dto.AuthorId);

			if (!user)
			{
				_logger.LogInformation($"User with id {dto.AuthorId} does not exist. UPDATE action cancelled.");
				throw new NotFoundException("User does not exist");
			}

			if (book is null)
			{
				_logger.LogInformation($"Book with id {dto.BookId} does not exist. UPDATE action cancelled.");
				throw new NotFoundException("Book not found");
			}

			if (!(dto.AuthorId is null) && !author)
			{
				_logger.LogInformation($"Author with id {dto.AuthorId} does not exist. UPDATE action cancelled.");
				throw new NotFoundException("Autor not found");
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

			book.ModUserId = (int)dto.ModUserId;

			_dbContext.SaveChanges();

			_logger.LogInformation($"Book with id {dto.BookId} successfully updated by user {dto.ModUserId}");

			return true;
		} // Modify()


	}
}
