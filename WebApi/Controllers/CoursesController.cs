using Application.CoursesFeatures.Commands;
using Application.CoursesFeatures.Queries;
using Application.ModelsDto;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CoursesController : ApiControllerBase
    {
        

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CourseDto>>> GetAll()
        {
            return await Mediator.Send(new GetAllCoursesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> CourseById(Guid Id)
        {
            return await Mediator.Send(new GetCourseByIdQuery{ Id = Id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateCourse(CreateCourseCommand course)
        {
            return await Mediator.Send(course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateCourse(Guid Id, UpdateCourseCommand course)
        {
            course.Id = Id;
            return await Mediator.Send(course);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCourse(int Id)
        {
            return await Mediator.Send(new DeleteCourseCommand { CourseId = Id });
        }
    }
}
