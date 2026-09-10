using FluentValidation;
using HabitTracker.Application.DTOs;

namespace HabitTracker.Application.Validators;

public sealed class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is not valid.");

        RuleFor(request => request.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(request => request.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}