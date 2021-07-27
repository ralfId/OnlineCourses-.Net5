using Application.HandlersApplication;
using Application.ModelsDto;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CoursesFeatures.Queries
{
    public class GetCourseByIdQuery : IRequest<CourseDto>
    {
        public Guid Id { get; set; }
    }

    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto>
    {
        private readonly OnlineCoursesContext _coursesContext;
        private readonly IMapper _mapper;

        public GetCourseByIdQueryHandler(OnlineCoursesContext coursesContext, IMapper mapper)
        {
            _coursesContext = coursesContext;
            _mapper = mapper;
        }
        public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _coursesContext.Courses
                .Include(x => x.CourseInstructor)
                .ThenInclude(x => x.Instructors)
                .FirstOrDefaultAsync(x=> x.CourseId == request.Id);

            if (course == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "The course not found" });
            }

            return _mapper.Map<Courses, CourseDto>(course);
        }
    }
}
