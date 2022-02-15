using AmberArchives.Entities;
using AmberArchives.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
	public interface IBookService
	{
		BookDto GetBook(int id);
		PageResult<BookDto> GetAll(BookQuery query);
		int Add(CreateBookDto dto);
		void Rate(RateBookDto dto);
		void Delete(int bookId, int userId);
		void Update(ModifyBookDto dto);
	}
}
