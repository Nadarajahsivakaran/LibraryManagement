using AutoMapper;
using Library.Models;
using Library.Models.DTO;
using System.Diagnostics.Metrics;

namespace Library.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<IssuingBooks, IssueBookDTO>().ReverseMap();

        }
    }
}
