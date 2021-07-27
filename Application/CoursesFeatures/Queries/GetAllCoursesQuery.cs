using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using System.Threading;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Application.ModelsDto;

namespace Application.CoursesFeatures.Queries
{
    public class GetAllCoursesQuery : IRequest<List<CourseDto>> { }

    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, List<CourseDto>>
    {
        private readonly OnlineCoursesContext _coursesContext;
        public GetAllCoursesQueryHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }
        public async Task<List<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _coursesContext.Courses
                .Include(x => x.courseInstructor)
                .ThenInclude(x => x.Instructors)
                .Select(x =>
               
                   new CourseDto()
                   {
                       CourseId = x.CourseId,
                       Title = x.Title,
                       Description = x.Description,
                       PublicationDate = x.PublicationDate,
                       CoverPhoto = x.CoverPhoto,
                       Instructors = new InstructorDto()
                       {
                           InstructorId = x.courseInstructor.Id
                       }
                   }
               ).ToListAsync();
        }
    }
}
