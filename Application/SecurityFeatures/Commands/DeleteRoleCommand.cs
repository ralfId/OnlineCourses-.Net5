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
    public class DeleteRoleCommand : IRequest
    {
        public string Name { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);

            if (role == null)
            {
                throw new HandlerExceptions(HttpStatusCode.BadRequest, new { messaje = "Role not exist" });
            }

            var deleted = await _roleManager.DeleteAsync(role);

            if (deleted.Succeeded)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { messaje = "Can't delete role" });
            }
            
        }
    }
}
