using ClinicService.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ClinicServiceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["DatabaseSettings:ConnectionString"]);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
            }

            app.MapControllers();

            app.Run();
        }
    }
}