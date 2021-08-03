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

namespace Application.SecurityFeatures.Queries
{
    public class GetRolesByUserQuerty : IRequest<List<string>>
    {
        public string UserName { get; set; }
    }

    public class GetRolesByUserQuertyHandler : IRequestHandler<GetRolesByUserQuerty, List<string>>
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public GetRolesByUserQuertyHandler(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<List<string>> Handle(GetRolesByUserQuerty request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "User not exist" });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new List<string>(roles);
        }
    }
}
