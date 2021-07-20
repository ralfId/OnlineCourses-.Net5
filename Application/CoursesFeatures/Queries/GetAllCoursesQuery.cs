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

namespace Application.CoursesFeatures.Queries
{
    public class GetAllCoursesQuery : IRequest<List<Courses>> { }

    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, List<Courses>>
    {
        private readonly OnlineCoursesContext _coursesContext;
        public GetAllCoursesQueryHandler(OnlineCoursesContext coursesContext )
        {
            _coursesContext = coursesContext;
        }
        public async Task<List<Courses>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _coursesContext.Courses.ToListAsync();
        }
    }
}
