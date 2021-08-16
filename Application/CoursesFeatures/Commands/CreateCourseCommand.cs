using Application.HandlersApplication;
using Domain.Models;
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
    public class CreateCourseCommand : IRequest
    {
        public Guid? CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public List<Guid> InstructorList { get; set; }
        public decimal Price { get; set; }
        public decimal PricePromotion { get; set; }


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
            Guid courseId = Guid.NewGuid();

            if (request.CourseId != null)
            {
                courseId = request.CourseId ?? Guid.NewGuid();
            }
            //create the new course
            var course = new Courses
            {
                CourseId = courseId,
                Title = request.Title,
                Description = request.Description,
                PublicationDate = request.PublicationDate,
                CreationDate = DateTime.UtcNow
            };

            _coursesContext.Courses.Add(course);

            //add instructors to the new course
            if (request.InstructorList != null)
            {
                request.InstructorList.ForEach(ci =>
                {
                    var coursInst = new CourseInstructor
                    {
                        CourseId = courseId,
                        InstructorId = ci
                    };
                    _coursesContext.CourseInstructor.Add(coursInst);
                });
            }

            //add price and pricepromotion to the new course
            var priceCourse = new Prices
            {
                PriceId = Guid.NewGuid(),
                CourseId = courseId,
                CurrentPrice = request.Price,
                Promotion = request.PricePromotion
            };

            _coursesContext.Prices.Add(priceCourse);


            var resp = await _coursesContext.SaveChangesAsync();

            if (resp > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can't create the course" });
            }


        }
    }
}
