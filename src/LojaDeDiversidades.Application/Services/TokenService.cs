using LojaDeDiversidades.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace LojaDeDiversidades.Application.Services;

using Microsoft.Extensions.Configuration;
using System.Security.Claims;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GerarToken(int usuarioId, string nome, string email, string role)
    {
        var jwtConfig = configuration.GetSection("Jwt");
        var key = Convert.FromBase64String(jwtConfig["Key"]);


        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
            new Claim(ClaimTypes.Name, nome),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = jwtConfig["Issuer"],
            Audience = jwtConfig["Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
