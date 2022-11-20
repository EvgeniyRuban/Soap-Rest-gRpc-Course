using AutoMapper;
using ClinicService.Api.Services;
using ClinicService.BusinessLogic.Services;
using ClinicService.DAL;
using ClinicService.DAL.Repos;
using ClinicService.Domain.Mappers;
using ClinicService.Domain.Repos;
using ClinicService.Domain.Repos.Interfaces;
using ClinicService.Domain.Services;
using Google.Api;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;

namespace ClinicService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Configure AutoMapper

        var mapperConfigurations = new MapperConfiguration(mp => mp.AddProfile<MappingProfile>());
        var mapper = mapperConfigurations.CreateMapper();
        builder.Services.AddSingleton(mapper);

        #endregion

        #region Configure Grpc

        // https://learn.microsoft.com/en-us/aspnet/core/grpc/json-transcoding?view=aspnetcore-7.0

        builder.Services.AddGrpc().AddJsonTranscoding();

        #endregion

        #region Configure Kestrel

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, 5101, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });
        });

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, 5102, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        #endregion

        #region Configure EF DbContext

        builder.Services.AddDbContext<ClinicServiceDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration["DatabaseSettings:ConnectionString"]);
        });

        #endregion

        #region Configure Swagger

        builder.Services.AddSwaggerGen();

        #endregion

        #region Registering service dependencies

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IAccountSessionRepository, AccountSessionRepository>();

        #endregion

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.UseRouting();

        app.Run();
    }
}