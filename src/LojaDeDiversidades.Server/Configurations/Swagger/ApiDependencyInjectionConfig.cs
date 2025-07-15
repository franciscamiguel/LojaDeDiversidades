using Asp.Versioning.ApiExplorer;
using LojaDeDiversidades.Api.Configurations.Identity;

namespace LojaDeDiversidades.Api.Configurations.Swagger;

public static class ApiDependencyInjectionConfig
{
    public static IServiceCollection ConfigureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors();

        services.AddControllers();
        services.AddVersionedSwagger();
        services.ConfigureAuthentication(configuration);

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseHttpsRedirection();

        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseVersionedSwagger(provider);
        app.UseIdentityConfiguration();

        if (app.Environment.IsEnvironment("Test"))
            app.MapControllers().AllowAnonymous();
        else
            app.MapControllers();

        return app;
    }
}