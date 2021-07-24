using Application.SecurityFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SecurityFeatures.Validations
{
    public class RegisterUserValidation :AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidation()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.LastName).NotEmpty();
            RuleFor(r => r.UserName).NotEmpty();
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}
