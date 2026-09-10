using System.Text;
using HabitTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql.Replication;

namespace HabitTracker.Api.Endpoints;

public static class ApiExtensions
{
    public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUserEndpoints();
    }

    public static void ApiAddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>() 
            ?? throw new InvalidOperationException("Jwt not found");


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
               options.TokenValidationParameters = new(){ 
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
               };
            });
        services.AddAuthorization();
        
    }
}