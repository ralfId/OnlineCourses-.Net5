using AutoMapper;
using Domain.Models;
using System.Linq;

namespace Application.ModelsDto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Courses, CourseDto>()
                .ForMember(x => x.Instructors, y => y.MapFrom(z => z.CourseInstructor.Select(a => a.Instructors).ToList()))
                .ForMember(x => x.Prices, y => y.MapFrom(z => z.Prices))
                .ForMember(x => x.Comments, y => y.MapFrom(z => z.Comments));

            CreateMap<Instructors, InstructorDto>();
            CreateMap<CourseInstructor, CourseInstructorDto>();
            CreateMap<Comments, CommentDto>();
            CreateMap<Prices, PriceDto>();
        }
    }
}
