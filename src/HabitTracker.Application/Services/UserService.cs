using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using HabitTracker.Application.Exceptions;
using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using HabitTracker.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Application.Services;

public class UserService
{
	private readonly UserManager<User> userManager;
	private readonly IJwtGenerator jwtGenerator;

	public UserService(UserManager<User> userManager, IJwtGenerator jwtGenerator)
	{
		this.userManager = userManager;
		this.jwtGenerator = jwtGenerator;
	}

	public async Task<(User?, IdentityResult)> Register(string email, string password)
	{
        var user = new User(email, password);
		var result = await userManager.CreateAsync(user, password);
		if (!result.Succeeded)
		{
            return (null, result);
        }
		await userManager.AddToRoleAsync(user, Roles.User.ToString());
		return (user, result);
	}

	
	public async Task<(User? User, string? Token)> Login(string email, string password)
	{
		var user = await userManager.FindByEmailAsync(email);
		if (user is null || !await userManager.CheckPasswordAsync(user, password))
		{
			return (null, null);
		}

		var roles = await userManager.GetRolesAsync(user);
		var token = jwtGenerator.CreateJwt(user, roles);
		return (user, token);
	}



}