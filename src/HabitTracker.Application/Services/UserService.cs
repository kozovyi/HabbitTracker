using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using HabitTracker.Application.Exceptions;
using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Services;

public class UserService
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IJwtGenerator _jwtGenerator;

	public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator)
	{
		_userRepository = userRepository; 
		_passwordHasher = passwordHasher; 
		_jwtGenerator = jwtGenerator; 
	}

	public async Task Register(string username, string email, string password)
	{
		if (await _userRepository.ExistsByEmailAsync(email))
		{
			throw new DuplicateUserEmailException(email);
		}
		var hashedPassword = _passwordHasher.Generate(password);
		var user = User.Create(email, username, hashedPassword);

		await _userRepository.AddAsync(user);
		await _userRepository.SaveChangesAsync();
	}

	public async Task<string> Login(string email, string password)
	{
		var user = await _userRepository.GetByEmailAsync(email)
			?? throw new UserNotFoundException(email);
		
		var isValidPassword = _passwordHasher.Verify(password, user.PasswordHash);
		// if (!isValidPassword)
		// 	throw new InvalidCredentialsException();

		var token = _jwtGenerator.CreateJwt(user);
		return token;
	}



}