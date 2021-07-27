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
                .ForMember(x => x.Instructors, y => y.MapFrom(z => z.CourseInstructor.Select(a => a.Instructors).ToList()));

            CreateMap<Instructors, InstructorDto>();

            CreateMap<CourseInstructor, CourseInstructorDto>();
        }
    }
}
