using System.ComponentModel.DataAnnotations;
using HabitTracker.Domain.Enums;

namespace HabitTracker.Application.DTOs;

public record CreateHabitDto(
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(150)]
    string Title,

    [MaxLength(1000)]
    string? Description,

    [Range(1, 7, ErrorMessage = "Number of days must be between 1 and 7")]
    int TargetDaysPerWeek
);
    
public record UpdateHabitDto(
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(150)]
    string Title,

    [MaxLength(1000)]
    string? Description,

    [Range(1, 7, ErrorMessage = "Number of days must be between 1 and 7")]
    int TargetDaysPerWeek
);

public record ChangeHabitStatusDto(
    [Required(ErrorMessage = "Status is required")]
    HabitStatus Status
);

public record HabitResponseDto(
    Guid Id,
    string Title,
    string Description,
    int TargetDaysPerWeek,
    HabitStatus Status,
    DateTime CreatedAtUtc
);

public record HabitSummaryDto(
    Guid Id,
    string Title,
    int TargetDaysPerWeek,
    HabitStatus Status
);