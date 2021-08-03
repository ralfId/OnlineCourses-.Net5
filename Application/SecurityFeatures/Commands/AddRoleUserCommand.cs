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
    public class AddRoleUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }

    public class AddRoleUserCommandHandler : IRequestHandler<AddRoleUserCommand>
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AddRoleUserCommandHandler(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Unit> Handle(AddRoleUserCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "Role not exist" });
            }

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "User not exist" });
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (result.Succeeded)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can't add role to user" });
            }
        }
    }
}
