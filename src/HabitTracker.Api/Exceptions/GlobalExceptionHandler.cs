using HabitTracker.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Api.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            AppException appEx => MapErrorType(appEx.ErrorType),
            _ => (StatusCodes.Status500InternalServerError, "Server Error")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Unhandled server error occurred.");
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = statusCode == StatusCodes.Status500InternalServerError 
                ? "An unexpected error occurred." 
                : exception.Message,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
    private static (int StatusCode, string Title) MapErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Conflict     => (StatusCodes.Status409Conflict, "Conflict"),
            ErrorType.NotFound     => (StatusCodes.Status404NotFound, "Not Found"),
            ErrorType.Unauthorized => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            ErrorType.Forbidden    => (StatusCodes.Status403Forbidden, "Forbidden"),
            ErrorType.Validation   => (StatusCodes.Status400BadRequest, "Validation Error"),
            _                      => (StatusCodes.Status400BadRequest, "Bad Request")
        };

}