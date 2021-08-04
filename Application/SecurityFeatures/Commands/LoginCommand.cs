using Application.Contracts;
using Application.HandlersApplication;
using Application.ResponseModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Commands
{
    public class LoginCommand : IRequest<UserData>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserData>
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginCommandHandler(
            UserManager<Users> userManager, 
            SignInManager<Users> signInManager, 
            IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<UserData> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new HandlerExceptions(HttpStatusCode.Unauthorized);
            }

            var resp = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!resp.Succeeded)
            {
                throw new HandlerExceptions(HttpStatusCode.Unauthorized);
            }

            var roles = await _userManager.GetRolesAsync(user);
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
