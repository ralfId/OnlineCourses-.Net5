using Application.HandlersApplication;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DocumentsFeatures.Commands
{
    public class UploadDocumentCommand : IRequest
    {
        public Guid ObjectReference { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public string Extention { get; set; }
    }

    public class UploadDocummenCommandHandler : IRequestHandler<UploadDocumentCommand>
    {
        private readonly OnlineCoursesContext _coursesContext;

        public UploadDocummenCommandHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }

        public async Task<Unit> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var userDoc = await _coursesContext.Documents.Where(x => x.ObjectReference == request.ObjectReference).FirstOrDefaultAsync();

            if (userDoc == null)
            {
                var newDoc = new Documents
                {
                    DocumentId = Guid.NewGuid(),
                    ObjectReference = request.ObjectReference,
                    Name= request.Name,
                    Content = Convert.FromBase64String(request.Data),
                    Extention = request.Extention,
                    CreationDate = DateTime.UtcNow
                };

                await _coursesContext.Documents.AddAsync(newDoc);
            }
            else
            {
                userDoc.Name = request.Name;
                userDoc.Content = Convert.FromBase64String(request.Data);
                userDoc.Extention = request.Extention;
            }

            var resp = await _coursesContext.SaveChangesAsync();

            if (resp > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.InternalServerError, new { message = "Can not upload the file" });
            }

        }

        
    }
}
