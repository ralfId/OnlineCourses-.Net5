using Application.HandlersApplication;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Commands
{
    public class LoginCommand : IRequest<Users>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Users>
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public LoginCommandHandler(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<Users> Handle(LoginCommand request, CancellationToken cancellationToken)
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

            return user;
        }
    }
}
