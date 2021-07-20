using Domain.Models;
using MediatR;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CoursesFeatures.Commands
{
    public class UpdateCourseCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
    }

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly OnlineCoursesContext _coursesContext;

        public UpdateCourseCommandHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }

        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseExist = await _coursesContext.Courses.FindAsync(request.Id);
            if (courseExist == null)
            {
                throw new Exception("Course don't exist");
            }


            courseExist.Title = request.Title ?? courseExist.Title;
            courseExist.Description = request.Description ?? courseExist.Description;
            courseExist.PublicationDate = request.PublicationDate ?? courseExist.PublicationDate;

            _coursesContext.Courses.Update(courseExist);
            var resp = await _coursesContext.SaveChangesAsync();

            if (resp > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new Exception("Can't update the course");
            }
        }
    }
}
