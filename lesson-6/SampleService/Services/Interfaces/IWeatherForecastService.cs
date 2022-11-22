using RootService.Remote;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> Get(CancellationToken stoppingToken = default);
}
