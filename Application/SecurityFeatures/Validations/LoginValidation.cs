using Application.SecurityFeatures.Commands;
using Domain.Models;
using FluentValidation;

namespace Application.SecurityFeatures.Validations
{
    public class LoginValidation : AbstractValidator<LoginCommand>
    {
        public LoginValidation()
        {
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.Password).NotEmpty();
        }
    }
}
