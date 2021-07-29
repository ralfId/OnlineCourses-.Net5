using MediatR;
using Persistence.DapperConfiguration.DapperModels;
using Persistence.Repository.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstructorsFeatures.Queries
{
    public class GetAllInstructorsQuery : IRequest<List<InstructorDM>>
    {
    }

    public class GetAllInstructorsQueryCommand : IRequestHandler<GetAllInstructorsQuery, List<InstructorDM>>
    {
        private readonly IInstructorRepository _instructorRepos;
        public GetAllInstructorsQueryCommand(IInstructorRepository instructorRepos)
        {
            _instructorRepos = instructorRepos;
        }
        public async Task<List<InstructorDM>> Handle(GetAllInstructorsQuery request, CancellationToken cancellationToken)
        {
            var instructorsList = await _instructorRepos.GetAllAsync();
            return instructorsList.ToList();
        }
    }
}
