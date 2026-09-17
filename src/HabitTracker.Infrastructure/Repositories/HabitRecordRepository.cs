


using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Repositories;

public sealed class HabitRecordRepository(ApplicationDbContext db) : IHabitRecordRepository
{
    public Task<HabitRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        db.HabitRecords
            .AsNoTracking()
            .SingleOrDefaultAsync(record => record.Id == id, cancellationToken);

    public Task<List<HabitRecord>> GetAllByHabitIdAsync(Guid habitId, CancellationToken cancellationToken = default) =>
        db.HabitRecords
            .AsNoTracking()
            .Where(record => record.HabitId == habitId)
            .OrderBy(record => record.CompletedAtUtc)
            .ToListAsync(cancellationToken);

    public Task<List<HabitRecord>> GetByHabitIdForPeriodAsync(
        Guid habitId,
        DateTime fromUtc,
        DateTime toUtc,
        CancellationToken cancellationToken = default) =>
        db.HabitRecords
            .AsNoTracking()
            .Where(record => record.HabitId == habitId
                        && record.CompletedAtUtc >= fromUtc
                        && record.CompletedAtUtc <= toUtc)
            .OrderBy(record => record.CompletedAtUtc)
            .ToListAsync(cancellationToken);

    public void Remove(HabitRecord habitRecord)
    {
        ArgumentNullException.ThrowIfNull(habitRecord);

        db.HabitRecords.Remove(habitRecord);
    }

    public async Task<HabitRecord> AddAsync(HabitRecord habitRecord, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(habitRecord);

        await db.HabitRecords.AddAsync(habitRecord, cancellationToken);
        return habitRecord;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        db.SaveChangesAsync(cancellationToken);
}