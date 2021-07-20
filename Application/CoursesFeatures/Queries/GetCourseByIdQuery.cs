using Domain.Models;
using MediatR;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CoursesFeatures.Queries
{
    public class GetCourseByIdQuery : IRequest<Courses>
    {
        public int Id { get; set; }
    }

    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Courses>
    {
        private readonly OnlineCoursesContext _coursesContext;

        public GetCourseByIdQueryHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }
        public async Task<Courses> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _coursesContext.Courses.FindAsync(request.Id);
        }
    }
}
