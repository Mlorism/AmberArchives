﻿using AmberArchives.Entities;
using AmberArchives.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
    public class BookService: IBookService
    {
		private readonly AmberArchivesDbContext _dbContext;
		private readonly IMapper _mapper;
		public BookService(AmberArchivesDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

        public BookDto GetBook(int id)
		{
			var book = _dbContext
				.Books
				.Include(b => b.Author)
				.Include(b => b.Editions)
				.FirstOrDefault(b => b.Id == id);

			if (book is null) return null;

			var result = _mapper.Map<BookDto>(book);
			return result;
		} // GetBook()

		public IEnumerable<BookDto> Get()
		{
			var books = _dbContext
				.Books
				.Include(b => b.Author)
				.Include(b => b.Editions)
				.ToList();

			var booksDto = _mapper.Map<List<BookDto>>(books);

			return booksDto;
		} // Get()

		public int Add(CreateBookDto dto)
		{			
			var book = _mapper.Map<Book>(dto);
			_dbContext.Books.Add(book);
			_dbContext.SaveChanges();

			return book.Id;
		} // Add()

		public bool Delete(int id)
		{
			var book = _dbContext
				.Books
				.FirstOrDefault(b => b.Id == id);

			if (book is null) return false;

			_dbContext.Books.Remove(book);
			_dbContext.SaveChanges();

			return true;
		} // Delete()

		public ModifyBookDto Modify(ModifyBookDto dto)
		{
			var book = _dbContext
				.Books
				.Include(b => b.Author)
				.FirstOrDefault(b => b.Id == dto.Id);

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

			var result = _mapper.Map<BookDto>(book);

			return dto;
		} // Modify()


	}
}