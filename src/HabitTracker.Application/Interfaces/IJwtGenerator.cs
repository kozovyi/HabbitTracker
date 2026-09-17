using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Interfaces;

public interface IJwtGenerator
{
    string CreateJwt(User user, IEnumerable<string> roles);
}