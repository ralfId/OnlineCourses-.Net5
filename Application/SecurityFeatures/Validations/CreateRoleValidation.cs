using Application.SecurityFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Validations
{
    public class CreateRoleValidation : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
