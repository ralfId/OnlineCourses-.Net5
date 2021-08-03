using Application.SecurityFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Validations
{
    public class RemoveRoleUserValidation : AbstractValidator<RemoveRoleUserCommand>
    {
        public RemoveRoleUserValidation()
        {
            RuleFor(x => x.UserName);
            RuleFor(x => x.RoleName);
        }
    }
}
