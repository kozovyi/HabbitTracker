namespace HabitTracker.Domain.Entities;

public class HabitLog
{
    public Guid Id { get; set; }
    public Guid HabitId { get; set; }
    public DateTime CompletedAtUtc { get; set; }
    public string? Notes { get; set; }

    public required Habit Habit { get; set; }
}