using Application.HandlersApplication;
using MediatR;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CoursesFeatures.Commands
{
    public class DeleteCourseCommand : IRequest
    {
        public Guid CourseId { get; set; }
    }

    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly OnlineCoursesContext _coursesContext;

        public DeleteCourseCommandHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }
        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var courseExist = await _coursesContext.Courses.FindAsync(request.CourseId);

            if (courseExist == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "Course don't exist!" });
            }

            try
            {
                //select each record on table  CourseInstructor and deleted
                _coursesContext.CourseInstructor
                    .Where(x => x.CourseId == request.CourseId)
                    .ToList()
                    .ForEach(x => _coursesContext.CourseInstructor.Remove(x));

                //deleted el course on table Courses
                _coursesContext.Courses.Remove(courseExist);
                var resp = await _coursesContext.SaveChangesAsync();

                if (resp > 0)
                {
                    return Unit.Value;
                }
                else
                {
                    throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can't delete the course" });
                }
            }
            catch (Exception ex)
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = ex.Message.ToString() });

            }
        }
    }
}
