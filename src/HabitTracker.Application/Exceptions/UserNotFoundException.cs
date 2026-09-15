namespace HabitTracker.Application.Exceptions;

public sealed class UserNotFoundException : AppException
{
    public UserNotFoundException(string email)
        : base($"User with email '{email}' was not found.", errorType: ErrorType.NotFound)
    {
        Email = email;
    }

    public string Email { get; }
}