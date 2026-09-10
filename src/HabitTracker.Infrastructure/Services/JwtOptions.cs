namespace HabitTracker.Infrastructure.Services;

public class JwtOptions
{
    public string Key {get; set;} = string.Empty;
    public int ExpireHours {get; set;}
}