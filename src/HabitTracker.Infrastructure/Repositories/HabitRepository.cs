


using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Repositories;

public sealed class HabitRepository(ApplicationDbContext db) : IHabitRepository
{
	private readonly ApplicationDbContext _dbContext = db ?? throw new ArgumentNullException(nameof(db));

	public void Update(Habit habit)
	{
		ArgumentNullException.ThrowIfNull(habit);

		_dbContext.Habits.Update(habit);
	}

	public void Remove(Habit habit)
	{
		ArgumentNullException.ThrowIfNull(habit);

		_dbContext.Habits.Remove(habit);
	}

	public async Task AddAsync(Habit habit, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(habit);

		await _dbContext.Habits.AddAsync(habit, cancellationToken);
	}

	public Task<Habit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
		_dbContext.Habits
			.AsNoTracking()
			.SingleOrDefaultAsync(habit => habit.Id == id, cancellationToken);

	public Task<List<Habit>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
		_dbContext.Habits
			.AsNoTracking()
			.Where(habit => habit.UserId == userId)
			.OrderBy(habit => habit.CreatedAtUtc)
			.ToListAsync(cancellationToken);

	public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
		_dbContext.SaveChangesAsync(cancellationToken);
}