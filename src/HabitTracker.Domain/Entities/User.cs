
namespace HabitTracker.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email {get; set;} = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<Habit> Habits { get; set; } = new();


    public static User Create(string email, string username, string passwordHash)
    {
        return new User
        {
            Email = email,
            Username = username,
            PasswordHash = passwordHash
        };
    }
}