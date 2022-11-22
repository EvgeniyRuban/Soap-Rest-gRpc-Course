using Microsoft.AspNetCore.Mvc;

namespace RootService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly string[] summaries = new []
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get(CancellationToken stoppingToken = default)
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                }).ToList();

            return Random.Shared.Next(2) == 0 
                ? Ok(forecast)
                : throw new Exception("Service side error.");
        } 
    }
}
