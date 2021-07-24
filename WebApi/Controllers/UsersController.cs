using Application.ResponseModels;
using Application.SecurityFeatures.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous] //specifies that all requests to this endpoint will be freely accessible
        public async Task<ActionResult<UserData>> Login(LoginCommand loginData)
        {
            return await Mediator.Send(loginData);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserData>> Register(RegisterUserCommand registerData)
        {
            return await Mediator.Send(registerData);
        }
    }
}
