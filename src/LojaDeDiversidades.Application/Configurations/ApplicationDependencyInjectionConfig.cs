using LojaDeDiversidades.Application.Interfaces;
using LojaDeDiversidades.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LojaDeDiversidades.Application.Configurations;

public static class ApplicationDependencyInjectionConfig
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        #region Domain.Notifier

        // services.AddScoped<INotifierMessage, NotifierMessage>();

        #endregion

        #region Domain.Validator

        // services.AddScoped<IValidatorGeneric, ValidatorFactory>();

        #endregion

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IVendaService, VendaService>();

        return services;
    }
}