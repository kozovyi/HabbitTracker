using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext db) : IUserRepository
{

	public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return	db.Users
			.AsNoTracking()
			.SingleOrDefaultAsync(user => user.Id == id, cancellationToken);
	}

	public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(email);

		return db.Users
			.AsNoTracking()
			.SingleOrDefaultAsync(user => user.Email == email.Trim(), cancellationToken);
	}

	public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(email);

		return db.Users
			.AsNoTracking()
			.AnyAsync(user => user.Email == email.Trim(), cancellationToken);
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(user);

		await db.Users.AddAsync(user, cancellationToken);
	}

	public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
		db.SaveChangesAsync(cancellationToken);
}
