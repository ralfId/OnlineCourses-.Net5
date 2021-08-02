using Application.SecurityFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class RolesController : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<Unit>> CreateRol(CreateRoleCommand rol)
        {
            return await Mediator.Send(rol);
        }
    }
}
