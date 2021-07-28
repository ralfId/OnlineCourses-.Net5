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
using AutoMapper;

namespace Application.CoursesFeatures.Queries
{
    public class GetAllCoursesQuery : IRequest<List<CourseDto>> { }

    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, List<CourseDto>>
    {
        private readonly OnlineCoursesContext _coursesContext;
        private readonly IMapper _mapper;

        public GetAllCoursesQueryHandler(OnlineCoursesContext coursesContext, IMapper mapper)
        {
            _coursesContext = coursesContext;
            _mapper = mapper;
        }
        public async Task<List<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _coursesContext.Courses
                .Include(x => x.Prices)
                .Include(x => x.Comments)
                .Include(x => x.CourseInstructor)
                .ThenInclude(x => x.Instructors).ToListAsync();

            return _mapper.Map<List<Courses>, List<CourseDto>>(courses);

        }
    }
}
