using Application.HandlersApplication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Commands
{
    public class CreateRoleCommand : IRequest
    {
        public string Name { get; set; }
    }

    public class CreateRolCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public CreateRolCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);

            if (role != null)
            {
                throw new HandlerExceptions(HttpStatusCode.BadRequest, new { message = "Role already exist" });
            }

            var createRole =  await _roleManager.CreateAsync(new IdentityRole(request.Name));

            if (createRole.Succeeded)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can't create the role" });
            }
        }
    }
}
