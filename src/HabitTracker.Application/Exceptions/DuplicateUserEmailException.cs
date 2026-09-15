namespace HabitTracker.Application.Exceptions;

public sealed class DuplicateUserEmailException : AppException
{
    public string Email { get; }

    public DuplicateUserEmailException(string email)
        : base($"User with email '{email}' already exists.", errorType: ErrorType.Conflict)
    {
        Email = email;
    }
}