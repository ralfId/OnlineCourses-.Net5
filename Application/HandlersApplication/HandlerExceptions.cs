using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.HandlersApplication
{
    public class HandlerExceptions : Exception
    {

        public HandlerExceptions(HttpStatusCode statusCode, object errors = null)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public HttpStatusCode StatusCode { get; }
        public object Errors { get; }

      
    }
}
