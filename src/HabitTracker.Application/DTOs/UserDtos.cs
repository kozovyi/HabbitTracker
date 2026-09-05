using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Application.DTOs;

public record RegisterUserDto
(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    string Email,

    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
    string Username,

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    string Password
);

public record LoginUserDto
(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    string Password
);

public record UserResponseDto
(
    Guid Id,
    string Email,
    string Username
);

public record AuthResponseDto
(
    string Token,
    UserResponseDto User
);