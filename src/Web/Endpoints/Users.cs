using Asp.Versioning.Builder;
using CleanArchitectureSample.Infrastructure.Identity;

namespace CleanArchitectureSample.Web.Endpoints;

public static class Users
{
    public static IEndpointRouteBuilder MapUsers(
        this IEndpointRouteBuilder app, ApiVersionSet versionSet)
    {
        app
            .MapGroup("/api/v{version:apiVersion}/users")
            .WithApiVersionSet(versionSet)
            .WithTags(nameof(Users))
            .WithOpenApi()
            .MapIdentityApi<ApplicationUser>();

        return app;
    }
}
