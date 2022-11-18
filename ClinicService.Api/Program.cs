using ClinicService.Api.Services;
using ClinicService.DAL;
using ClinicService.DAL.Repos;
using ClinicService.Domain.Repos;
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

        builder.Services.AddGrpcSwagger();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "ClinicService", Version = "v1" });
            var filePath = Path.Combine(System.AppContext.BaseDirectory, "ClinicService.Api.xml");
            c.IncludeXmlComments(filePath);
            c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
        });

        #endregion

        #region Registering service dependencies

        builder.Services.AddScoped<IClientRepository, ClientRepository>();

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

        app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

        app.MapGrpcService<ClientService>().EnableGrpcWeb();

        app.Run();
    }
}