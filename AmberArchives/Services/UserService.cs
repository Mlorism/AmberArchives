using AmberArchives.Entities;
using AmberArchives.Models;
using AmberArchives.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Services
{
    public class UserService : IUserService
    {
		private readonly AmberArchivesDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public UserService(AmberArchivesDbContext dbContext, IMapper mapper, ILogger<BookService> logger)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_logger = logger;
		}

		public void Rate(RateBookDto dto)
		{
			var bookRate = _mapper.Map<BookRating>(dto);
			var user = _dbContext
				.Users
				.Include(u => u.Ratings)
				.FirstOrDefault(u => u.Id == dto.ModUserId);
			// verify user and book existence
			var rating = user.Ratings.FirstOrDefault(r => r.UserId == dto.ModUserId);

			if (rating is null)
			{
				user.Ratings.Add(bookRate);
			}
			else
			{
				rating.Rating = dto.Rating;
			}
			// calculate new rating for book
			_dbContext.SaveChanges();

		} // Rate()
	}
}
