using Application.ResponseModels;
using Application.SecurityFeatures.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(LoginCommand loginCommand)
        {
            return await Mediator.Send(loginCommand);
        }
    }
}
