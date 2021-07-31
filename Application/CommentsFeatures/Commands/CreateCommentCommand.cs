using Application.HandlersApplication;
using Domain.Models;
using MediatR;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommentsFeatures.Commands
{
    public class CreateCommentCommand : IRequest
    {
        public string Student { get; set; }
        public int Score { get; set; }
        public string CommentText { get; set; }
        public Guid CourseId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly OnlineCoursesContext _coursesContext;
        public CreateCommentCommandHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comments
            {
                CommentId = Guid.NewGuid(),
                Student = request.Student,
                Score = request.Score,
                CommentText = request.CommentText,
                CourseId = request.CourseId,
                CreationDate = DateTime.UtcNow
            };

            await _coursesContext.Comments.AddAsync(comment);

            var result = await _coursesContext.SaveChangesAsync();

            if (result > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can´t create comment" });
            }
        }
    }
}
