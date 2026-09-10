using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Interfaces;

public interface IJwtGenerator
{
    public string CreateJwt(User user);
}