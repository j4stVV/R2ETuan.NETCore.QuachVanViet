using Microsoft.Extensions.Options;
using R2ETuan.NETCore.QuachVanViet.Configuration;
using R2ETuan.NETCore.QuachVanViet.Middleware;
using R2ETuan.NETCore.QuachVanViet.Services;
using R2ETuan.NETCore.QuachVanViet.Services.Logging;

namespace R2ETuan.NETCore.QuachVanViet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add logging
            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });

            //Sign in LoggingOptions and bind value from configuration
            builder.Services.Configure<LoggingOptions>(builder.Configuration.GetSection("Logging"));

            builder.Services.AddSingleton<ILoggingOptions>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<LoggingOptions>>();
                return options.Value;
            });

            //Sign in IRequestLogger with implementation FileRequestLogger
            builder.Services.AddSingleton<IRequestLogger, FileRequestLogger>();

            //Sign in IEmployeeService with implementation EmployeeService
            builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            //Configure the HTTP request pipeline
            app.UseMiddleware<LoggingMiddleware>();

            app.MapControllers();

            app.MapGet("/", () => "Hello World");

            app.Run();
        }
    }
}
