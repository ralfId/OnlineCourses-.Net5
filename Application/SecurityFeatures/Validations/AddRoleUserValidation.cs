using Application.SecurityFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Validations
{
    public class AddRoleUserValidation : AbstractValidator<AddRoleUserCommand>
    {
        public AddRoleUserValidation()
        {
            RuleFor(x => x.RoleName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
