using LojaDeDiversidades.Domain.Interfaces;
using LojaDeDiversidades.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LojaDeDiversidades.Infra.Configurations;

public static class RegisterInfraServices
{
    public static void ConfigureInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LojaDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IVendaRepository, VendaRepository>();

    }
}