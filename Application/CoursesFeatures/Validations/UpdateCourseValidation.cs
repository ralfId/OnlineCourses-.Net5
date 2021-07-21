using Application.CoursesFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesFeatures.Validations
{
    public class UpdateCourseValidation : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseValidation()
        {
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.PublicationDate).NotEmpty();
        }
    }
}
