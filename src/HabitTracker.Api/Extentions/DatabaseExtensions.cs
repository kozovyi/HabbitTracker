using HabitTracker.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Api.Extentions;
public static class DatabaseExtensions
{
    public static async Task SeedRolesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        
        foreach(Roles role in Enum.GetValues<Roles>())
        {
            if(!await roleManager.RoleExistsAsync(role.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role.ToString()));
            }
        }
    }
}