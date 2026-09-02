
namespace HabitTracker.Domain.Entities;

public class HabitReminder
{
    public Guid Id {get; set;}
    public TimeOnly ReminderTime {get; set;}
    public bool IsEnable {get; set;}
    public Guid HabitId {get; set;}
    
    public required Habit Habit {get; set;}
}