using Application.HandlersApplication;
using Domain.Models;
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
    public class RemoveRoleUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }

    public class RemoveRoleUserCommandHandler : IRequestHandler<RemoveRoleUserCommand>
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RemoveRoleUserCommandHandler(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Unit> Handle(RemoveRoleUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "User not exist" });
            }

            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "Role not exist" });
            }

            var resp = await _userManager.RemoveFromRoleAsync(user, request.RoleName);
            if (resp.Succeeded)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Could not remove role to user" }); ;

            }
        }
    }
}
