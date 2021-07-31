using Application.HandlersApplication;
using MediatR;
using Persistence.DapperConfiguration.DapperModels;
using Persistence.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstructorsFeatures.Queries
{
    public class GetInstructorByIdQuery : IRequest<InstructorDM>
    {
        public Guid InstructorId { get; set; }
    }

    public class GetInstructorByIdQueryCommand : IRequestHandler<GetInstructorByIdQuery, InstructorDM>
    {
        private readonly IInstructorRepository _instructorRepos;
        public GetInstructorByIdQueryCommand(IInstructorRepository instructorRepos)
        {
            _instructorRepos = instructorRepos;
        }
        public async Task<InstructorDM> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorRepos.GetItemByIdAsync(request.InstructorId);
            if (instructor != null)
            {
                return instructor; ;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can't get an instructor" });
            }
        }
    }
}
