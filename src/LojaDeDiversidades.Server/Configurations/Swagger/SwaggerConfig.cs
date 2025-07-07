using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LojaDeDiversidades.Api.Configurations.Swagger;

public static class SwaggerConfig
{
    public static IServiceCollection AddVersionedSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

        //add swagger json generation
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(p => p.FullName);

            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                var version = typeof(SwaggerConfig).Assembly.GetName().Version;
                var apiInfoDescription = new StringBuilder("Loja de Diversidades");
                //describe the info
                var info = new OpenApiInfo
                {
                    Title = $"LojaDeDiversidades {description.GroupName}",
                    Version = version?.ToString(),
                    Description = apiInfoDescription.ToString(),
                    License = new OpenApiLicense() { Name = $"App Version: {version?.Major}.{version?.Minor}.{version?.Build}" }
                };

                if (description.IsDeprecated)
                {
                    apiInfoDescription.Append(" This API version has been deprecated.");
                    info.Description = apiInfoDescription.ToString();
                }

                options.SwaggerDoc(description.GroupName, info);
                options.OperationFilter<DefaultHeaderFilter>();

            }

            //set default for non body parameters
            options.ParameterFilter<DefaultParametersFilter>();

            //add security definition
            var securityDefinition = new OpenApiSecurityScheme
            {
                Description = "Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            };

            options.AddSecurityDefinition("Bearer", securityDefinition);

            var openApiSecurityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                openApiSecurityScheme,
                Array.Empty<string>()
            }});
        });

        return services;
    }

    public static IApplicationBuilder UseVersionedSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(
            options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    options.RoutePrefix = string.Empty;
                }
            });

        return app;
    }
}
