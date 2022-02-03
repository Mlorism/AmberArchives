using AmberArchives.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
	public interface IEditionService
	{
		int Create(int bookId, CreateEditionDto dto);
	}
}
