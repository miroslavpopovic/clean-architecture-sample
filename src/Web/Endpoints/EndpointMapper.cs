namespace CleanArchitectureSample.Web.Endpoints;

public static class EndpointMapper
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        
        var versionSet = app.GetDefaultApiVersionSet();

        app
            .MapTodoLists(versionSet)
            .MapTodoItems(versionSet)
            .MapUsers(versionSet)
            .MapWeatherForecasts(versionSet);
    }
}
