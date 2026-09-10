namespace HabitTracker.Application.DTOs;

public record RegisterUserDto
(
    string Email,
    string Username,
    string Password
);

public record LoginUserDto
(
    string Email,
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