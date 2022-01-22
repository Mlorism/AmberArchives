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
			CreateMap<Author, AuthorDto>();
			CreateMap<Edition, EditionDto>();
			CreateMap<CreateBookDto, Book>();
		}
    }
}
