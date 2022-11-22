using Microsoft.AspNetCore.HttpLogging;
using NLog.Web;
using Polly;
using SampleService.Options;
using SampleService.Services;

namespace SampleService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #region Configure Options

        builder.Services.Configure<ServiceReferenciesUrlsOptions>(
            builder.Configuration.GetSection(nameof(ServiceReferenciesUrlsOptions)));

        #endregion

        #region Configure Logger

        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
            logging.RequestHeaders.Add("Authorization");
            logging.RequestHeaders.Add("X-Real-IP");
            logging.RequestHeaders.Add("X-Forwarded-For");
        });
        builder.Host.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();

        }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

        #endregion

        #region Configure HttpClient

        builder.Services.AddHttpClient<IWeatherForecastService, WeatherForecastService>()
            .AddTransientHttpErrorPolicy(config =>
                config.WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: (attemptCount) => TimeSpan.FromSeconds(attemptCount * 2),
                    onRetry: (response, sleepDuration, attemptNumber, context) =>
                    {
                        var logger = builder.Services.BuildServiceProvider().GetService<ILogger<WeatherForecastService>>();

                        logger.LogError(response.Exception is not null
                            ? response.Exception
                            : new Exception($"\n{response.Result.StatusCode}: {response.Result.RequestMessage}"),
                                $"(attempt: {attemptNumber}) {nameof(WeatherForecastService)} request exception.");
                    }));

        #endregion

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.MapControllers();

        app.Run();
    }
}