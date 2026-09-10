namespace HabitTracker.Application.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string email)
        : base($"User with email '{email}' was not found.")
    {
        Email = email;
    }

    public string Email { get; }
}