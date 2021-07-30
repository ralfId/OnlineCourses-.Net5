using Application.InstructorsFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstructorsFeatures.Validations
{
    public class UpdateInstructorValidation : AbstractValidator<UpdateInstructorCommand>
    {
        public UpdateInstructorValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Lastname).NotEmpty();
            RuleFor(x => x.Degree).NotEmpty();
        }
    }
}
