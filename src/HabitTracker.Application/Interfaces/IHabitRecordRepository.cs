using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Interfaces;

public interface IHabitRecordRepository
{
    Task<HabitRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<HabitRecord>> GetAllByHabitIdAsync(Guid habitId, CancellationToken cancellationToken = default);
    Task<List<HabitRecord>> GetByHabitIdForPeriodAsync(Guid habitId, DateTime fromUtc, DateTime toUtc, CancellationToken cancellationToken = default);
    void Remove(HabitRecord habitRecord);
    Task<HabitRecord> AddAsync(HabitRecord habitRecord, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}