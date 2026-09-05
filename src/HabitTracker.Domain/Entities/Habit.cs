
namespace HabitTracker.Domain.Entities;

using HabitTracker.Domain.Enums;

public class Habit
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TargetDaysPerWeek { get; set; }
    public HabitStatus Status { get; set; } = HabitStatus.Active;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }

    public required User User { get; set; }
    public HabitReminder? HabitReminder { get; set; }
    public List<HabitRecord> Records { get; set; } = new();

}