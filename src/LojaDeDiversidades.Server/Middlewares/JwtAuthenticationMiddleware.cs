using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LojaDeDiversidades.Api.Middlewares;

public class JwtAuthenticationMiddleware(IConfiguration configuration) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = ExtractToken(context);

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var principal = ValidateToken(token);
                if (principal != null)
                    context.User = principal;
                else
                    Console.WriteLine("Token inválido recebido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao validar token: {ex.Message}");

                // Diferentes tipos de erro JWT
                if (ex.Message.Contains("expired"))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token expirado");
                    return;
                }
                else if (ex.Message.Contains("audience"))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token com audience inválido");
                    return;
                }
                else if (ex.Message.Contains("issuer"))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token com issuer inválido");
                    return;
                }
                else if (ex.Message.Contains("signature"))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token com assinatura inválida");
                    return;
                }
            }
        }

        await next(context);
    }

    private static string ExtractToken(HttpContext context)
    {
        var authHeader = context.Request.Headers.Authorization.FirstOrDefault();

        if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return authHeader["Bearer ".Length..].Trim();
        }

        return null;
    }

    private ClaimsPrincipal ValidateToken(string token)
    {
        var jwtConfig = configuration.GetSection("Jwt");
        var key = Convert.FromBase64String(jwtConfig["Key"]);

        var tokenHandler = new JwtSecurityTokenHandler();

        // Configuração mais simples que funciona no .NET 8
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}