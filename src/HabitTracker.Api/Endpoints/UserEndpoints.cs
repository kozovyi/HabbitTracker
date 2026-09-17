using FluentValidation;
using HabitTracker.Application.DTOs;
using HabitTracker.Application.Services;

namespace HabitTracker.Api.Endpoints;


public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");
        group.MapPost("/register", Register);
        group.MapPost("/login", Login);
        group.MapPost("/me", Me).RequireAuthorization();
        return app;
    }

    private static async Task<IResult?> ValidateAsync<T>(T request, IValidator<T> validator)
    {
        var result = await validator.ValidateAsync(request);
        return result.IsValid
            ? null
            : Results.ValidationProblem(result.Errors.GroupBy(error => error.PropertyName).ToDictionary(group => group.Key, group => group.Select(error => error.ErrorMessage).ToArray()));   
    }

    private static async Task<IResult> Register(
        RegisterUserDto request,
        UserService userService,
        IValidator<RegisterUserDto> validator,
        CancellationToken cancellationToken)
    {
        var validationError = await ValidateAsync(request, validator);
        if (validationError != null) return validationError;
        
        var (user, result) = await userService.Register(email:request.Email.Trim(), request.Password);
        if (!result.Succeeded)
            {
                var errors = result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());

                return Results.ValidationProblem(errors);
            }

        return Results.Created($"/auth/users/{user!.Id}", new UserResponseDto(user.Id, user.Email!, user.UserName!));
    }

    private static async Task<IResult> Login(
        LoginUserDto request,
        IValidator<LoginUserDto> validator,
        UserService userService)
    {
        var validationErrors = await ValidateAsync(request, validator);
        if (validationErrors is not null)
            return validationErrors;

        var (user, token) = await userService.Login(request.Email.Trim(), request.Password);
        if (user is null || token is null)
            return Results.Unauthorized();

        return Results.Ok(new AuthResponseDto(token, new UserResponseDto(user.Id, user.Email!, user.UserName!)));
    }

    private static IResult Me(HttpContext context) => Results.Ok(new
    {
        UserId = context.User.FindFirst("userId")?.Value,
        Email = context.User.FindFirst(System.Security.Claims.ClaimValueTypes.Email)?.Value,
        Username = context.User.Identity?.Name,
        Roles = context.User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(claim => claim.Value)
    });

}