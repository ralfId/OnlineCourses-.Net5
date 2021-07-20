using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly OnlineCoursesContext _coursesContext;

        public WeatherForecastController(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }

        [HttpGet]
        public IEnumerable<Courses> Get()
        {
            return _coursesContext.Courses.ToList();
        }
    }
}
