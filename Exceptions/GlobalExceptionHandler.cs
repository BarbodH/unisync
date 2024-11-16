using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace UniSyncApi.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsync(new ExceptionDetails
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = "Something went wrong on the server side."
        }.ToString(), cancellationToken: cancellationToken);

        return true;
    }
}