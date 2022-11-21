using AutoMapper;
using ClinicService.BusinessLogic.Services;
using ClinicService.DAL;
using ClinicService.DAL.Repos;
using ClinicService.Domain.Mappers;
using ClinicService.Domain.Repos;
using ClinicService.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

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

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = typeof(Program).Assembly.GetName().Name,
                Version = "v1"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Autorization header using the Bearer scheme (Example: 'Bearer 12345abcde')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>() 
                }
            });
        });

        #endregion

        #region Configure JWT

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationService.SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });

        #endregion

        #region Registering service dependencies

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IAccountSessionRepository, AccountSessionRepository>();
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

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

        app.UseAuthentication();

        app.UseAuthorization();

        app.Run();
    }
}