using Application.Contracts;
using Application.HandlersApplication;
using Application.ResponseModels;
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
    public class GetCurrentUserQuerty : IRequest<UserData>
    {
    }

    public class GetCurrentUserQuertyHandler : IRequestHandler<GetCurrentUserQuerty, UserData>
    {
        private readonly UserManager<Users> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ICurrentUser _currentUser;

        public GetCurrentUserQuertyHandler(UserManager<Users> userManager, IJwtGenerator jwtGenerator, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _currentUser = currentUser;
        }
        public async Task<UserData> Handle(GetCurrentUserQuerty request, CancellationToken cancellationToken)
        {
            var userName = _currentUser.GetCurrentUser();
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can't get current user" });
            }

            var roles =await _userManager.GetRolesAsync(user);
            var rolesList = new List<string>(roles);
            return new UserData
            {
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Token = _jwtGenerator.CreateToken(user, rolesList),
                Image = null
            };
        }
    }
}
