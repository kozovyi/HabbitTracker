namespace HabitTracker.Domain.Exceptions;

public class HabitNotFoundException : Exception
{
    public HabitNotFoundException(Guid habitId)
        : base($"Habit with id '{habitId}' was not found.")
    {
        HabitId = habitId;
    }

    public Guid HabitId { get; }
}