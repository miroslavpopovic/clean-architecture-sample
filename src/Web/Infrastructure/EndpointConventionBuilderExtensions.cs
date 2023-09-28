namespace CleanArchitectureSample.Web.Infrastructure;

public static class EndpointConventionBuilderExtensions
{
    /// <summary>
    /// Adds an OpenAPI annotation to <see cref="Endpoint.Metadata" /> associated
    /// with the current endpoint.
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    /// <param name="builder">The <see cref="IEndpointConventionBuilder"/>.</param>
    /// <param name="summary">The operation summary.</param>
    /// <param name="description">The operation description.</param>
    /// <param name="parameterDescriptions">The description for each operation parameter, if any.</param>
    public static void WithOpenApi<TBuilder>(
        this TBuilder builder, string summary, string description, params string[] parameterDescriptions)
        where TBuilder : IEndpointConventionBuilder
    {
        builder.WithOpenApi(operation =>
        {
            operation.Summary = summary;
            operation.Description = description;

            var parameterLength = parameterDescriptions.Length;

            for (var i = 0; i < parameterLength; i++)
            {
                operation.Parameters[i].Description = parameterDescriptions[i];
            }

            return operation;
        });
    }
}
