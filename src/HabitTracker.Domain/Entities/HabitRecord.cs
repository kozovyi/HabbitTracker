
namespace HabitTracker.Domain.Entities;

public class HabitRecord
{
    public Guid Id {get; set;}
    public Guid HabitId {get; set;}
    public DateTime CompletedAtUtc {get;set;}
    public string? Notes {get; set;} = null; 

    public required Habit Habit {get; set;}

}