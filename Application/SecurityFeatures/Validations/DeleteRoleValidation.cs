using Application.SecurityFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Validations
{
    public class DeleteRoleValidation : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidation()
        {
            RuleFor(x => x.Name);
        }
    }
}
