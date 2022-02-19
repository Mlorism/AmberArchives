using AmberArchives.Entities;
using AmberArchives.Models;
using AmberArchives.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
	public class ShelfService : IShelfService
	{
		private readonly AmberArchivesDbContext _dbContext;
		private readonly IMapper _mapper;
		public ShelfService(AmberArchivesDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public List<ShelfDto> GetShelfs()
		{
			var shelfs = _dbContext.Shelfs.ToList();
			var result = _mapper.Map<List<ShelfDto>>(shelfs);

			return result;
		} // GetShelfs()
	}
}
