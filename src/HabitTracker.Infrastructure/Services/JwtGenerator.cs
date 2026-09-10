using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HabitTracker.Infrastructure.Services;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtOptions _options;
    public JwtGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }        

    public string CreateJwt(User user)
    {   
        Claim[] claims = [new("userId", user.Id.ToString())];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
            SecurityAlgorithms.HmacSha256);
        var tokenObject = new JwtSecurityToken(signingCredentials: signingCredentials, expires: DateTime.UtcNow.AddHours(_options.ExpireHours), claims: claims);
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(tokenObject);
        return tokenValue;
    }       
}