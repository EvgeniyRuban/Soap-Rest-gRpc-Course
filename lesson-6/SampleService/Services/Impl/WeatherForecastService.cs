using Microsoft.Extensions.Options;
using RootService.Remote;
using SampleService.Options;

namespace SampleService.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly RootServiceClient _rootClient;
    private readonly ILogger _logger;


    public WeatherForecastService(
        HttpClient httpClient,
        IOptions<ServiceReferenciesUrlsOptions> options,
        ILogger<WeatherForecastService> logger)
    {
        _rootClient = new RootServiceClient(options.Value.RootService, httpClient);
        _logger = logger;
    }


    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken stoppingToken = default)
        => await _rootClient.WeatherForecastAsync(stoppingToken);
}