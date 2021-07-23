using Application.HandlersApplication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Middlewares
{
    public class HandlerErrorsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerErrorsMiddleware> _logger;

        public HandlerErrorsMiddleware(RequestDelegate next, ILogger<HandlerErrorsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                await HandlerExceptionAsync(httpContext, ex, _logger);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext httpContext, Exception ex, ILogger<HandlerErrorsMiddleware> logger)
        {
            object error = null;

            switch (ex)
            {
                //HTTP errors
                case HandlerExceptions he:
                    logger.LogError(ex, "Handle HTTTP Exception ");
                    error = he.Errors;
                    httpContext.Response.StatusCode = (int)he.StatusCode;
                    break;
                case Exception e:
                    logger.LogError(ex, "Server Error");
                    error = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            httpContext.Response.ContentType = "application/json";

            if (error != null)
            {
                var result = JsonConvert.SerializeObject(error);
                await httpContext.Response.WriteAsync(result);
            }
        }
        
    }
}
