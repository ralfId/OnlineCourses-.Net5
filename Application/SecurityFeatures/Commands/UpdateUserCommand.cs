using System;
using Application.Contracts;
using Application.HandlersApplication;
using Application.ResponseModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Commands
{
    public class UpdateUserCommand : IRequest<UserData>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ProfileImage ProfileImage { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserData>
    {
        private readonly OnlineCoursesContext _coursesContext;
        private readonly UserManager<Users> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IPasswordHasher<Users> _passwordHasher;
        public UpdateUserCommandHandler(OnlineCoursesContext coursesContext,
            UserManager<Users> userManager,
            IJwtGenerator jwtGenerator,
            IPasswordHasher<Users> passwordHasher)
        {
            _coursesContext = coursesContext;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserData> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //user in db
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "User not exist" });
            }

            //if email already exist in db
            var email = await _coursesContext.Users.Where(x => x.Email == request.Email && x.UserName != request.UserName).AnyAsync();
            if (email)
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "This email is in use" });
            }

            if (request.ProfileImage != null)
            {
                var userImage = await _coursesContext.Documents.Where(x => x.ObjectReference == new Guid(user.Id)).FirstOrDefaultAsync();

                if (userImage == null)
                {
                    var newUserImage = new Documents
                    {
                        DocumentId = Guid.NewGuid(),
                        ObjectReference = new Guid(user.Id),
                        Name = request.ProfileImage.Name,
                        Extention = request.ProfileImage.Extention,
                        Content = Convert.FromBase64String(request.ProfileImage.Data),
                        CreationDate = DateTime.UtcNow
                    };
                    await _coursesContext.Documents.AddAsync(newUserImage);
                }
                else
                {
                    userImage.Name = request.ProfileImage.Name;
                    userImage.Content = Convert.FromBase64String(request.ProfileImage.Data);
                    userImage.Extention = request.ProfileImage.Extention;
                }
            }


            user.Name = request.Name;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            var resp = await _userManager.UpdateAsync(user);

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
                    ProfileImage = profileImage ?? null,
                    Token = _jwtGenerator.CreateToken(user, rolesList)
                };
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = resp.Errors.ToString() });
            }


        }
    }
}