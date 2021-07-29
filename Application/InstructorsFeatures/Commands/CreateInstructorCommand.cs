using Application.HandlersApplication;
using MediatR;
using Persistence.Repository.IServices;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstructorsFeatures.Commands
{
    public class CreateInstructorCommand : IRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Degree { get; set; }
    }

    public class CreateInstructorCommandHandler : IRequestHandler<CreateInstructorCommand>
    {
        private readonly IInstructorRepository _instructorRepos;
        public CreateInstructorCommandHandler(IInstructorRepository instructorRepos)
        {
            _instructorRepos = instructorRepos;
        }
        public async Task<Unit> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
        {
            var result = await _instructorRepos.CreateItemAsync(request.Name, request.Lastname, request.Degree);
            if (result > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can´t create the instructor" });
            }
        }
    }
}
