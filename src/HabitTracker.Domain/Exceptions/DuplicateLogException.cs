namespace HabitTracker.Domain.Exceptions;

public class DuplicateLogException : Exception
{
    public DuplicateLogException(Guid habitId, DateTime completedAtUtc)
        : base($"Habit '{habitId}' already has a log for {completedAtUtc:yyyy-MM-dd}.")
    {
        HabitId = habitId;
        CompletedAtUtc = completedAtUtc;
    }

    public Guid HabitId { get; }
    public DateTime CompletedAtUtc { get; }
}