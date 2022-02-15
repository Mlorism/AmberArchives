using AmberArchives.Entities;
using AmberArchives.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives
{
    public class AmberArchivesMappingProfile : Profile
    {
		public AmberArchivesMappingProfile()
		{
			CreateMap<Book, BookDto>();
			CreateMap<CreateBookDto, Book>();
			CreateMap<RateBookDto, BookRating>()
				.ForMember(m => m.UserId, c => c.MapFrom(s => s.ModUserId));
			CreateMap<Edition, EditionDto>();
			CreateMap<CreateEditionDto, Edition>();
			CreateMap<Author, AuthorDto>();
		}
    }
}
