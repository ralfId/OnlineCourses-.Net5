using Application.Contracts;
using Application.HandlersApplication;
using Application.ResponseModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Commands
{
    public class RegisterUserCommand : IRequest<UserData>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserData>
    {
        private readonly OnlineCoursesContext _coursesContext;
        private readonly UserManager<Users> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        public RegisterUserCommandHandler(
            OnlineCoursesContext coursesContext,
            UserManager<Users> userManager,
            IJwtGenerator jwtGenerator)
        {
            _coursesContext = coursesContext;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<UserData> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userNameExist = await _coursesContext.Users.Where(un => un.UserName == request.UserName).AnyAsync();
            if (userNameExist)
            {
                throw new HandlerExceptions(HttpStatusCode.BadRequest, new { message = "User name is in use" });

            }
              
            var emailExist = await  _coursesContext.Users.Where(e => e.Email == request.Email).AnyAsync();
            if (emailExist)
            {
                throw new HandlerExceptions(HttpStatusCode.BadRequest, new { message = "Email already in use" });
            }

            var user = new Users
            {
                Name =request.Name,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName
            };

            var createUser = await _userManager.CreateAsync(user, request.Password);

            if (!createUser.Succeeded)
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = createUser.Errors.FirstOrDefault() });
            }

            return new UserData
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.LastName,
                UserName = user.UserName,
                Token = _jwtGenerator.CreateToken(user, null),
                Image = null
            };
        }
    }
}
