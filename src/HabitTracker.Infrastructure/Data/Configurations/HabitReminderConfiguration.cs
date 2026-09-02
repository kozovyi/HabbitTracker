using HabitTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitTracker.Infrastructure.Data.Configurations;

public class HabitReminderConfiguration : IEntityTypeConfiguration<HabitReminder>
{
	public void Configure(EntityTypeBuilder<HabitReminder> builder)
	{
        builder.ToTable("habit_reminders");
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.Habit)
            .WithOne(h => h.HabitReminder)
            .HasForeignKey<HabitReminder>(r => r.HabitId)
            .OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(reminder => reminder.HabitId)
			.IsUnique();
	}
}
