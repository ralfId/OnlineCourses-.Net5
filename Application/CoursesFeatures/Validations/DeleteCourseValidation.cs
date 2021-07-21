using Application.CoursesFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesFeatures.Validations
{
    public class DeleteCourseValidation : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseValidation()
        {
            RuleFor(c => c.CourseId).NotNull();
        }
    }
}
