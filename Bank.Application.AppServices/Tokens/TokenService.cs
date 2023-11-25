using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Bank.Application.AppServices.Abstractions.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Application.AppServices.Tokens;

public class TokenService : ITokenService
{
    public string GenerateJwtToken(string userId, string login, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, login),
                new Claim(ClaimTypes.Name, login),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }
}