
using Microsoft.AspNetCore.Identity;
namespace HabitTracker.Domain.Entities;

public class User: IdentityUser<Guid>
{
    public ICollection<Habit> Habits { get; private set; } = new List<Habit>();
    public User() { }
    public User(string email, string username)
    {
        Id = Guid.NewGuid();
        Email = email;
        UserName = username;
    }


}