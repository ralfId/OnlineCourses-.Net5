using Application.CoursesFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesFeatures.Validations
{
    public class CreateCourseValidation : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseValidation()
        {
            RuleFor(r => r.CourseId).NotEmpty();
            RuleFor(r => r.Title).NotEmpty().WithMessage("{PropertyName} should not be empty ");
            RuleFor(r => r.Description).NotEmpty().WithMessage("{PropertyName} should not be empty ");
            RuleFor(r => r.PublicationDate).NotEmpty().WithMessage("{PropertyName} should not be empty ");

        }
    }
}
