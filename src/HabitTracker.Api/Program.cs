using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using HabitTracker.Api.Endpoints;
using HabitTracker.Api.Exceptions;
using HabitTracker.Application.Interfaces;
using HabitTracker.Application.Services;
using HabitTracker.Application.DTOs;
using HabitTracker.Application.Validators;
using HabitTracker.Infrastructure.Data;
using HabitTracker.Infrastructure.Repositories;
using HabitTracker.Infrastructure.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHabitRepository, HabitRepository>();
builder.Services.AddScoped<IHabitRecordRepository, HabitRecordRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();
builder.Services.ApiAddAuthentication(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.AddMappedEndpoints();

app.Run();

