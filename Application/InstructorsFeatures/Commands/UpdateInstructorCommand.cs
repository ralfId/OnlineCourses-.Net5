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
    public class UpdateInstructorCommand : IRequest
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Degree { get; set; }
    }

    public class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand>
    {
        private readonly IInstructorRepository _instructorRepos;
        public UpdateInstructorCommandHandler(IInstructorRepository instructorRepos)
        {
            _instructorRepos = instructorRepos;
        }
        public async Task<Unit> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            var result = await _instructorRepos.UpdateItemAsync(
                                request.InstructorId,request.Name, request.Lastname, request.Degree);
            if (result > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can´t update the instructor" });
            }
        }
    }
}
