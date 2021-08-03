using Application.SecurityFeatures.Commands;
using Application.SecurityFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

        [HttpDelete("delete")]
        public async Task<ActionResult<Unit>> DeleteRol(DeleteRoleCommand role)
        {
            return await Mediator.Send(role);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<IdentityRole>>> GetAllRoles()
        {
            return await Mediator.Send(new GetAllRolesQuery());
        }

        [HttpPost("enrolluser")]
        public async Task<ActionResult<Unit>> EnrollUser(AddRoleUserCommand addRole)
        {
            return await Mediator.Send(addRole);
        }
    }
}
