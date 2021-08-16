using System;
using System.Threading.Tasks;
using Application.DocumentsFeatures.Queries;
using Microsoft.AspNetCore.Mvc;
using Application.ModelsDto;
using Application.DocumentsFeatures.Commands;
using MediatR;

namespace WebApi.Controllers
{
    public class DocumentsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocument(Guid id)
        { 
            return await Mediator.Send(new GetDocumentQuery{ObjectReference = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateDocument(UploadDocumentCommand data){
            return await Mediator.Send(data);
        }
    }
}