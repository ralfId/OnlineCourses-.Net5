using Application.CommentsFeatures.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommentsFeatures.Validations
{
    public class CreateCommentValidation : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidation()
        {
            RuleFor(x => x.Student).NotEmpty();
            RuleFor(x => x.CourseId).NotEmpty();
            RuleFor(x => x.Score).NotEmpty();
            RuleFor(x => x.CommentText).NotEmpty();

        }
    }
}
