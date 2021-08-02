using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Queries
{
    public class GetAllRolesQuery : IRequest<List<IdentityRole>>
    {
    }

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<IdentityRole>>
    {
        private readonly OnlineCoursesContext _coursesContext;
        public GetAllRolesQueryHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }

        public async Task<List<IdentityRole>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _coursesContext.Roles.ToListAsync();

        }
    }
}
