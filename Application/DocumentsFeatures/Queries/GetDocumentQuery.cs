using Application.HandlersApplication;
using Application.ModelsDto;
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

namespace Application.DocumentsFeatures.Queries
{
    public class GetDocumentQuery : IRequest<DocumentDto>
    {
        public Guid ObjectReference { get; set; }
    }

    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, DocumentDto>
    {
        private readonly OnlineCoursesContext _coursesContext;

        public GetDocumentQueryHandler(OnlineCoursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }

        public async Task<DocumentDto> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            var userDoc = await _coursesContext.Documents.Where(x => x.ObjectReference == request.ObjectReference).FirstOrDefaultAsync();
            if (userDoc != null)
            {
                return new DocumentDto
                {
                    Name = userDoc.Name,
                    Content = Convert.ToBase64String(userDoc.Content),
                    Extention = userDoc.Extention
                };
            }
            else
            {
                throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "User's file not exist" });
            }
        }
    }
}
