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
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
        {
            _logger.LogError(exception,"An exception occurred while processing the request.");
            var statusCode = StatusCodes.Status500InternalServerError;
            var response = new ProblemDetails
            {
                Title = "An error occurred while processing your request."
            };
            switch (exception)
            {
                case MongoAuthenticationException:
                case MongoConnectionException:
                    statusCode = StatusCodes.Status503ServiceUnavailable;
                    break;
                case MongoWriteException mongoException
                    when mongoException.WriteError?.Code == 11000 && mongoException.WriteError.Message.Contains("ux_users_email"):
                    statusCode = StatusCodes.Status409Conflict;
                    response.Title = "Email already exists";
                    response.Detail = "A user with this email is already registered.";
                    break;
                case MongoException:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            response.Status = statusCode;
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(response,cancellationToken);
            return true;
        }
    }
}