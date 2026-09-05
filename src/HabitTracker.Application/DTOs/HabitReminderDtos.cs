using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Application.DTOs;

public record CreateHabitReminderDto
(
    [Required] Guid HabitId,
    [Required] TimeOnly ReminderTime,
    bool IsEnabled = true
);

public record UpdateHabitReminderDto
(
    TimeOnly? ReminderTime,
    bool? IsEnabled
);

public record HabitReminderResponseDto
(
    Guid Id,
    Guid HabitId,
    TimeOnly ReminderTime,
    bool IsEnabled
);