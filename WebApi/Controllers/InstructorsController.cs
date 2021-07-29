using Application.InstructorsFeatures.Commands;
using Application.InstructorsFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.DapperConfiguration.DapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class InstructorsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<InstructorDM>>> GetAllInstructors()
        {
            return await Mediator.Send(new GetAllInstructorsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateInstructor(CreateInstructorCommand instructor)
        {
            return await Mediator.Send(instructor);
        }

    }
}
