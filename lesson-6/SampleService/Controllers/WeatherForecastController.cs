using Microsoft.AspNetCore.Mvc;
using RootService.Remote;

namespace SampleService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;


        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }


        [HttpGet]
        public async Task<ActionResult<ICollection<WeatherForecast>>> Get(CancellationToken stoppingToken = default)
        {
            return Ok(await _weatherForecastService.Get(stoppingToken));
        }
    }
}
