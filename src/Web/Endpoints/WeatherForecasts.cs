using Asp.Versioning.Builder;
using CleanArchitectureSample.Application.WeatherForecasts;

namespace CleanArchitectureSample.Web.Endpoints;

public static class WeatherForecasts
{
    public static void MapWeatherForecasts(this IEndpointRouteBuilder app, ApiVersionSet versionSet)
    {
        var group = app
            .MapGroup("/api/v{version:apiVersion}/weather-forecasts")
            .WithApiVersionSet(versionSet)
            .WithTags(nameof(WeatherForecast))
            .WithOpenApi()
            .RequireAuthorization();

        group
            .MapGet("/", GetWeatherForecasts)
            .WithName(nameof(GetWeatherForecasts))
            .WithOpenApi(
                "Get weather forecasts.",
                "Gets a list of weather forecasts.");
    }

    public static async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(ISender sender)
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }
}
