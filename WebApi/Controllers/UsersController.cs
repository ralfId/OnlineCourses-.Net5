using Application.SecurityFeatures.Commands;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login(LoginCommand loginCommand)
        {
            return await Mediator.Send(loginCommand);
        }
    }
}
