using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Interfaces;

public interface IHabitRecordRepository
{
    Task<HabitLog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<HabitRecord> GetAllByHabitId(Guid habitId, CancellationToken cancellationToken = default);
    Task<List<HabitLog>> GetByHabitIdForPeriodAsync(Guid habitId, DateTime fromUtc, DateTime toUtc, CancellationToken cancellationToken = default);
    void Remove(HabitRecord habitRecord);
    Task<HabitRecord> AddAsync(HabitRecord habitRecord, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}