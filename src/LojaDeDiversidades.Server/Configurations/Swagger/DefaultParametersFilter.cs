using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LojaDeDiversidades.Api.Configurations.Swagger;

public class DefaultParametersFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (parameter is not { } nonBodyParameter) return;

        nonBodyParameter.Description ??= context.ApiParameterDescription.ModelMetadata.Description;

        if (context.ApiParameterDescription.RouteInfo != null)
        {
            parameter.Required |= !context.ApiParameterDescription.RouteInfo.IsOptional;
        }
    }
}