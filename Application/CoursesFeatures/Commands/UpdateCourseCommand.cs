using Application.HandlersApplication;
using AutoMapper;
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
    public class UpdateCourseCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<Guid> InstructorsList { get; set; }
        public decimal? Price { get; set; }
        public decimal? PricePromotion { get; set; }
    }

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly OnlineCoursesContext _coursesContext;
        private readonly IMapper _mapper;

        public UpdateCourseCommandHandler(OnlineCoursesContext coursesContext, IMapper mapper)
        {
            _coursesContext = coursesContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseExist = await _coursesContext.Courses.FindAsync(request.Id);
            if (courseExist == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NoContent, new { message = "Course not found"});
            }

            //update commun properties
            courseExist.Title = request.Title ?? courseExist.Title;
            courseExist.Description = request.Description ?? courseExist.Description;
            courseExist.PublicationDate = request.PublicationDate ?? courseExist.PublicationDate;
            courseExist.CreationDate = DateTime.UtcNow;

            //update prices
            var priceCourse = _coursesContext.Prices.Where(x => x.CourseId == courseExist.CourseId).FirstOrDefault();

            if (priceCourse!= null)
            {
                priceCourse.CurrentPrice = request.Price ?? priceCourse.CurrentPrice;
                priceCourse.Promotion = request.PricePromotion ?? priceCourse.Promotion;
            }
            else
            {
                var newPriceCourse = new Prices
                {
                    PriceId = Guid.NewGuid(),
                    CourseId = courseExist.CourseId,
                    CurrentPrice = request.Price ?? 0,
                    Promotion = request.PricePromotion ?? 0
                };

               await _coursesContext.Prices.AddAsync(newPriceCourse);
            }

            //update instructors
            if (request.InstructorsList != null && request.InstructorsList.Count > 0)
            {
                //remove old instrutors in a course
                var oldInstructors = _coursesContext.CourseInstructor.Where(x => x.CourseId == request.Id).ToList();
                oldInstructors.ForEach(x => _coursesContext.CourseInstructor.Remove(x));

                // add/update new instructors in a course
                var newInstructors = request.InstructorsList.ToList();
                newInstructors.ForEach(x =>
                {
                    _coursesContext.CourseInstructor.Add(new CourseInstructor
                    {
                        CourseId = request.Id,
                        InstructorId = x
                    });
                });

            }

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
