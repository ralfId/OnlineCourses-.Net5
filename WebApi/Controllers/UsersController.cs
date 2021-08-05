using Application.ResponseModels;
using Application.SecurityFeatures.Commands;
using Application.SecurityFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [AllowAnonymous] //specifies that all requests to this endpoint will be freely accessible
    public class UsersController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(LoginCommand loginData)
        {
            return await Mediator.Send(loginData);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserData>> Register(RegisterUserCommand registerData)
        {
            return await Mediator.Send(registerData);
        }

        [HttpGet]
        public async Task<ActionResult<UserData>> GetCurrentUser()
        {
            return await Mediator.Send(new GetCurrentUserQuerty());
        }

        [HttpPut("update")]
        public async Task<ActionResult<UserData>> UpdateUser(UpdateUserCommand updateUser)
        {
            return await Mediator.Send(updateUser);
        }
    }
}
