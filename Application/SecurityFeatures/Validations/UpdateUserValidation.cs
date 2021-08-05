using System.Data;
using Application.SecurityFeatures.Commands;
using FluentValidation;

namespace Application.SecurityFeatures.Validations
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidation()
        {

            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x=>x.LastName).NotEmpty();
            RuleFor(x=>x.UserName).NotEmpty();
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();
            RuleFor(x=>x.Password).NotEmpty();
        }
        
    }
}