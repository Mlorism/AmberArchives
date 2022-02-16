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
		bool Add(CreateBookDto dto);
		bool Delete(int bookId, int userId);
		bool Update(ModifyBookDto dto);
	}
}
