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
		bool Delete(DeleteElementDto dto);
		bool Update(ModifyBookDto dto);
	}
}
