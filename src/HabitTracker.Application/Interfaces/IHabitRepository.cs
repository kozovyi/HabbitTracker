
using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Interfaces;

public interface IHabitRepository
{
    void Update(Habit habit);
    void Remove(Habit habit);
    Task AddAsync(Habit habit, CancellationToken cancellationToken = default);
    Task<Habit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Habit>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}