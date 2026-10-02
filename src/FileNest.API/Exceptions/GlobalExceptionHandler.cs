using FileNest.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace FileNest.API.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An exception occurred while processing the request.");
            var statusCode = exception switch
            {
                AppException appException => appException.StatusCode,
                MongoAuthenticationException => StatusCodes.Status503ServiceUnavailable,
                MongoConnectionException => StatusCodes.Status503ServiceUnavailable,
                MongoWriteException => StatusCodes.Status409Conflict,
                MongoException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
            var response = new ProblemDetails
            {
                Title = exception is AppException
                    ? exception.Message
                    : "An error occurred while processing your request.",

                Status = statusCode
            };
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}