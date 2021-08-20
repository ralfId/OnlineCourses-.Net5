using Application.CoursesFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class ExpDocsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Stream>> GetCoursesPDF()
        {
            return await Mediator.Send(new CoursesPDFReportQuery());
        }
    }
}
