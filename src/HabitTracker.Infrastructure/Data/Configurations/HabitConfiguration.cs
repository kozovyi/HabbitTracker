using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Infrastructure.Data.Configurations;

public class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.ToTable("habits");
        builder.HasKey(h=>h.Id);

        builder.Property(h => h.Title)
            .HasMaxLength(150)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(h => h.Description)
            .HasMaxLength(1000);

        builder.HasOne(habit => habit.User)
            .WithMany(user => user.Habits)
            .HasForeignKey(habit => habit.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}