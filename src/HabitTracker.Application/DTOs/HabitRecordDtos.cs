using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Application.DTOs;

public record CreateHabitRecordDto
(
    [Required] 
    Guid HabitId,
    [Required] 
    DateTime CompletedAtUtc,
    string? Notes
);

public record UpdateHabitRecordDto
(
    DateTime? CompletedAtUtc,
    string? Notes
);

public record HabitRecordResponseDto
(
    Guid Id,
    Guid HabitId,
    DateTime CompletedAtUtc,
    string? Notes
);