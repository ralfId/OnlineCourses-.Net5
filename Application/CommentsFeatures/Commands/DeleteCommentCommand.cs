using Application.HandlersApplication;
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
    public class DeleteCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }
    }

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly OnlineCoursesContext _coursesContext;
        public DeleteCommentCommandHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var commentExist = await _coursesContext.Comments.FindAsync(request.CommentId);

            if (commentExist == null)
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "Can't find the comment" });
            }

            _coursesContext.Comments.Remove(commentExist);

            var result = await _coursesContext.SaveChangesAsync();

            if (result > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Cant't delete the comment" });
            }

        }
    }
}
