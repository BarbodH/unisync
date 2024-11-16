using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace UniSyncApi.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            InvalidFieldException => (int)HttpStatusCode.BadRequest,
            AuthenticationException or NotAuthenticatedException => (int)HttpStatusCode.Unauthorized,
            AccountNoLongerExistsException => (int)HttpStatusCode.Forbidden,
            ResourceNotFoundException => (int)HttpStatusCode.NotFound,
            DuplicateResourceException => (int)HttpStatusCode.Conflict,
            ResourceCreationException => (int)HttpStatusCode.InternalServerError,
            _ => (int)HttpStatusCode.InternalServerError
        };
        httpContext.Response.ContentType = "application/json";
        
        await httpContext.Response.WriteAsync(new ExceptionDetails
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = exception.Message
        }.ToString(), cancellationToken: cancellationToken);

        return true;
    }
}