using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Promax.Api.To.NetCore.Api.Filters
{
    [ExcludeFromCodeCoverage]
    internal class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            int statusCode;
            var result = context.Exception.Message;

            switch (context.Exception)
            {
                case ArgumentNullException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ArgumentException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException _:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case InvalidOperationException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    result = default;
                    _logger.LogError(context.Exception, "Erro inesperado");
                    break;
            }

            context.Result = new ObjectResult(result)
            {
                StatusCode = statusCode
            };
        }
    }
}
