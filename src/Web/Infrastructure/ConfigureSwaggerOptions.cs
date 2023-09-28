using System.Reflection;
using System.Text;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitectureSample.Web.Infrastructure;

// Source: https://github.com/dotnet/aspnet-api-versioning/tree/main/examples/AspNetCore/WebApi/MinimalOpenApiExample
/// <summary>
/// Configures the Swagger generation options.
/// </summary>
/// <remarks>This allows API versioning to define a Swagger document per API version after the
/// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </summary>
    /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger
    /// documents.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        // add a swagger document for each discovered API version
        // note: you might choose to skip or document deprecated API versions differently
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        options.DescribeAllParametersInCamelCase();

        options.IncludeXmlComments(
            Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var text = new StringBuilder("A Clean Architecture sample.");
        var info = new OpenApiInfo
        {
            Title = "Clean Architecture Sample",
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact
            {
                Name = "Miroslav Popovic",
                Url = new Uri("https://miroslavpopovic.com/")
            },
            License = new OpenApiLicense
            {
                Name = "Sample license",
                Url = new Uri("https://github.com/miroslavpopovic/clean-architecture-sample/blob/main/LICENSE")
            }
        };

        if (description.IsDeprecated)
        {
            text.Append(" This API version has been deprecated.");
        }

        if (description.SunsetPolicy is { } policy)
        {
            if (policy.Date is { } when)
            {
                text.Append(" The API will be sunset on ")
                    .Append(when.Date.ToShortDateString())
                    .Append('.');
            }

            if (policy.HasLinks)
            {
                text.AppendLine();

                foreach (var link in policy.Links)
                {
                    if (link.Type != "text/html") continue;

                    text.AppendLine();

                    if (link.Title.HasValue)
                    {
                        text.Append(link.Title.Value).Append(": ");
                    }

                    text.Append(link.LinkTarget.OriginalString);
                }
            }
        }

        info.Description = text.ToString();

        return info;
    }
}
