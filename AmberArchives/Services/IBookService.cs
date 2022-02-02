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
		IEnumerable<BookDto> Get();
		BookDto GetBook(int id);
		int Add(CreateBookDto dto);
		void Delete(int id);
		void Update(ModifyBookDto dto);
	}
}
