using AmberArchives.Entities;
using AmberArchives.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Controllers
{
	public static class ControllerHelper
	{

		public static class Messages {
			public static string idToSmall = "Item Ids cannot be lower than 1.";			
			public static string idDontExist = "id don't exist.";			
			public static string bookDuplicate = "This book alredy exist. Duplicate is not allowed.";			
		}

		public static bool BookDuplicate(IEnumerable<Book> bookList, CreateBookDto dto)
		{
			Book result = bookList.FirstOrDefault(b => b.OriginalTitle == dto.OriginalTitle && b.AuthorId == dto.AuthorId);

			if (result is null)
			{
				return false;
			}

			return true;
		}

	}
}
