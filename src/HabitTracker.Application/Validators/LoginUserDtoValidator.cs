using FluentValidation;
using HabitTracker.Application.DTOs;

namespace HabitTracker.Application.Validators;

public sealed class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is not valid.");

        RuleFor(request => request.Password)
            .NotEmpty();
    }
}