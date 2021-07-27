using AutoMapper;
using Domain.Models;

namespace Application.ModelsDto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Courses, CourseDto>();
            CreateMap<Instructors, InstructorDto>();
            CreateMap<CourseInstructor, CourseInstructorDto>();
        }
    }
}
