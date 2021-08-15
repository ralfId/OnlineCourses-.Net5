using Application.DocumentsFeatures.Commands;
using FluentValidation;

namespace Application.DocumentsFeatures.Validations
{
    public class UploadDocumentValidation : AbstractValidator<UploadDocumentCommand>
    {
        public UploadDocumentValidation()
        {
            RuleFor(x=> x.ObjectReference).NotEmpty();
            RuleFor(x=> x.Name).NotEmpty();
            RuleFor(x=> x.Data).NotEmpty();
            RuleFor(x=> x.Extention).NotEmpty();

        }
        
    }
}