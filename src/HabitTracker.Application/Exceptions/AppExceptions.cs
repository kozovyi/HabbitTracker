namespace HabitTracker.Application.Exceptions;

public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    Forbidden
}

public abstract class AppException : Exception
{
    public ErrorType ErrorType {get;}

    protected AppException(string message, ErrorType errorType) : base(message)
    {
        ErrorType = errorType;
    }
}