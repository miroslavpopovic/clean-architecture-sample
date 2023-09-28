using Asp.Versioning;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;

namespace CleanArchitectureSample.Web.Infrastructure;

public static class VersioningExtensions
{
    private static readonly ApiVersion[] AllApiVersions = { new(1), new(2) };
    private static ApiVersionSet? _defaultApiVersionSet; 

    public static void AddVersioning(this IServiceCollection services) =>
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = AllApiVersions.Last();
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

    // ReSharper disable once UnusedParameter.Global
    public static ApiVersion[] GetAllApiVersions(this IEndpointRouteBuilder app) => AllApiVersions;
    public static ApiVersionSet GetDefaultApiVersionSet(this IEndpointRouteBuilder app) =>
        _defaultApiVersionSet ??= app.NewApiVersionSet().HasApiVersions(AllApiVersions).Build();
}
