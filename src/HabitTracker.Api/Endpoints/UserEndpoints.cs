using FluentValidation;
using HabitTracker.Application.DTOs;
using HabitTracker.Application.Exceptions;
using HabitTracker.Application.Services;
using HabitTracker.Infrastructure.Repositories;

namespace HabitTracker.Api.Endpoints;


public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);   
        app.MapPost("login", Login).RequireAuthorization();

        return app;
    }
    
    private static async Task<IResult> Register(
        RegisterUserDto request,
        IValidator<RegisterUserDto> validator,
        UserService userService)
    {
        var validationErrors = await ValidateAsync(request, validator);
        if (validationErrors is not null)
            return Results.ValidationProblem(validationErrors);

        await userService.Register(request.Username, request.Email, request.Password);
        return Results.Ok();
    }

    private static async Task<IResult> Login(
        LoginUserDto request,
        IValidator<LoginUserDto> validator,
        UserService userService,
        HttpContext context)
    {
        var validationErrors = await ValidateAsync(request, validator);
        if (validationErrors is not null)
            return Results.ValidationProblem(validationErrors);

        try
        {
            var token = await userService.Login(request.Email, request.Password);
            context.Response.Cookies.Append("__Host-access_token", token, new CookieOptions
            {
                HttpOnly = true,                   
                Secure = true,                           
                SameSite = SameSiteMode.Strict,         
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                Path = "/"
            });
            return Results.Ok(token);

        }
        catch (UserNotFoundException)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "User not found");
        }
        // catch (InvalidCredentialsException)
        // {
        //     return Results.Problem(
        //         statusCode: StatusCodes.Status401Unauthorized,
        //         title: "Invalid credentials");
        // }
    }

    private static async Task<Dictionary<string, string[]>?> ValidateAsync<T>(T request, IValidator<T> validator)
    {
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
            return null;

        return result.Errors
            .GroupBy(error => error.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.ErrorMessage).ToArray());
    }


}