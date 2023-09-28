using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitectureSample.Web.Infrastructure;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
            .AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                
            });

        services.AddFluentValidationRulesToSwagger();
    }
    
    public static void UseVersionedSwagger(this WebApplication app)
    {
        var apiVersions = app.GetAllApiVersions();
        
        app.UseSwagger();
        app.UseSwaggerUI(
            options =>
            {
                foreach (var description in apiVersions)
                {
                    var url = $"/swagger/v{description.MajorVersion}/swagger.json";
                    var name = $"V{description.MajorVersion}";
                    options.SwaggerEndpoint(url, name);
                }
            });
    }
}
