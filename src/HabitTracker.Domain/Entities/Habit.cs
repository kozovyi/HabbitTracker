
namespace HabitTracker.Domain.Entities;
public class Habit
{
    public Guid Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public int TargetDaysPerWeek {get; set;}
    public DateTime CreatedAtUtc {get; set;}
    public Guid UserId {get; set;}

    public required User User {get; set;}
    public HabitReminder? HabitReminder {get; set;} = null;
    public List<HabitRecord> Records {get; set;} = new();

}