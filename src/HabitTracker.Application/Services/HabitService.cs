using HabitTracker.Application.Interfaces;

namespace HabitTracker.Application.Services;

public class HabitService
{
    private readonly IHabitRepository _habitRepository;

    public HabitService(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }
}
