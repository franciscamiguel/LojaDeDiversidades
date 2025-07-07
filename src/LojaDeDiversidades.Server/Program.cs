using LojaDeDiversidades.Api.Configurations.Swagger;
using LojaDeDiversidades.Application.Configurations;
using LojaDeDiversidades.Infra.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureApi(builder.Configuration)
    .ConfigureApplication()
    .ConfigureInfra(builder.Configuration);

var app = builder.Build();

await app.UseApi()
    .RunAsync();

public partial class Program;
