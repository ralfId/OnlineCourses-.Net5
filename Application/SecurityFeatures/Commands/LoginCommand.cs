using Application.Contracts;
using Application.HandlersApplication;
using Application.ResponseModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly OnlineCoursesContext _coursesContext;

        public LoginCommandHandler(
            UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            IJwtGenerator jwtGenerator,
            OnlineCoursesContext coursesContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _coursesContext = coursesContext;
        }
        public async Task<UserData> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new HandlerExceptions(HttpStatusCode.Unauthorized, new { message = "User not exist, please create your accout" });
            }

            var resp = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (resp.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var rolesList = new List<string>(roles);

                //get user image profile if exist
                ProfileImage profileImage = new ProfileImage();

                var userImage = await _coursesContext.Documents.Where(x => x.ObjectReference == new Guid(user.Id)).FirstOrDefaultAsync();

                if (userImage != null)
                {
                    profileImage.Name = userImage.Name;
                    profileImage.Data = Convert.ToBase64String(userImage.Content);
                    profileImage.Extention = userImage.Extention;
                }

                return new UserData
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user, rolesList),
                    ProfileImage = profileImage ?? null
                };
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.Unauthorized, new { message = "Something went wrong. Check your email and password!" });
            }


        }
    }
}
