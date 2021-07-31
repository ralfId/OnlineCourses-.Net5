using Application.CommentsFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CommentsController : ApiControllerBase
    {
       [HttpPost]
       public async Task<ActionResult<Unit>> CreateComment(CreateCommentCommand comment)
        {
            return await Mediator.Send(comment);
        }

       [HttpDelete("{id}")]
       public async Task<ActionResult<Unit>> DeleteComment(Guid id)
        {
            return await Mediator.Send(new DeleteCommentCommand { CommentId = id });
        }
    }
}
