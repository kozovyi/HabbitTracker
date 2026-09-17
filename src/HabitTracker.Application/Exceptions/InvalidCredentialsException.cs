namespace HabitTracker.Application.Exceptions;

public sealed class InvalidCredentialsException : AppException
{
    public InvalidCredentialsException(
        string message = "Invalid email or password.")
        : base(message, ErrorType.Unauthorized)
    {
    }
}