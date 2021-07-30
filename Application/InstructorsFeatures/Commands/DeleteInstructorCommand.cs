using Application.HandlersApplication;
using MediatR;
using Persistence.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstructorsFeatures.Commands
{
    public class DeleteInstructorCommand : IRequest
    {
        public Guid InstructorId { get; set; }
    }

    public class DeleteInstrucorCommandHandler : IRequestHandler<DeleteInstructorCommand>
    {
        private readonly IInstructorRepository _instructorRepos;
        public DeleteInstrucorCommandHandler(IInstructorRepository instructorRepos)
        {
            _instructorRepos = instructorRepos;
        }
        public async Task<Unit> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            var result = await _instructorRepos.DeleteItemAsync(request.InstructorId);

            if (result > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can't delete the course" });
            }
        }
    }
}
