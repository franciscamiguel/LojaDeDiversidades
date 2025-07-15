using LojaDeDiversidades.Api.Middlewares;

namespace LojaDeDiversidades.Api.Configurations.Identity;

public static class AuthenticationConfig
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<JwtAuthenticationMiddleware>();
        services.AddAuthorization();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseMiddleware<JwtAuthenticationMiddleware>();
        app.UseAuthorization();
    }
}