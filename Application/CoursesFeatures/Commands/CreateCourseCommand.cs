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
    public class CreateCourseCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }

    }

    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
    {
        private readonly OnlineCoursesContext _coursesContext;

        public CreateCourseCommandHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }
        public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Courses
            {
                Title = request.Title,
                Description = request.Description,
                PublicationDate = request.PublicationDate
            };

            _coursesContext.Courses.Add(course);
            var resp = await _coursesContext.SaveChangesAsync();
            if (resp > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new Exception("Can't save course");
            }
            

        }
    }
}
