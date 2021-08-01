using MediatR;
using Persistence.Pagination.Models;
using Persistence.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CoursesFeatures.Queries
{
    public class PaginationQuery : IRequest<PaginationModel>
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public int CuantityElements { get; set; }
    }

    public class PaginationQuertyHandler : IRequestHandler<PaginationQuery, PaginationModel>
    {
        private readonly IPaginationRepository _paginationRepos;
        public PaginationQuertyHandler(IPaginationRepository paginationRepos)
        {
            _paginationRepos = paginationRepos;
        }

        public async Task<PaginationModel> Handle(PaginationQuery request, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("CourseName", request.Title);

            return await _paginationRepos.returnPageAsync(
                "sp_Get_Pagination",
                request.PageNumber,
                request.CuantityElements,
                parameters,
                "Title");
        }
    }
}
