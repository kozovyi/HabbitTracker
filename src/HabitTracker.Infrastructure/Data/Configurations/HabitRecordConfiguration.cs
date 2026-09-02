using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Infrastructure.Data.Configurations;

public class HabitRecordConfiguration : IEntityTypeConfiguration<HabitRecord>
{
    public void Configure(EntityTypeBuilder<HabitRecord> builder)
    {
        builder.ToTable("habit_records");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Notes).HasMaxLength(500);
        
        builder.HasOne(record => record.Habit)
            .WithMany(habits => habits.Records)
            .HasForeignKey(record => record.HabitId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}