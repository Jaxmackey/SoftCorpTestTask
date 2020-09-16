using System;
using System.IO;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParserGismeteoService.Driver;
using ParserGismeteoService.Repo;
using Repository;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace ParserGismeteoService
{
    public class Program
    {
        public IConfigurationRoot Configuration { get; }
        public static void Main(string[] args)
        {
            const string loggerTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u4}]<{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var logfile = Path.Combine(baseDir, "App_Data", "logs", "log.txt");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.FromLogContext()
                .WriteTo.Console(LogEventLevel.Information, loggerTemplate, theme: AnsiConsoleTheme.Literate)
                .WriteTo.File(logfile, LogEventLevel.Information, loggerTemplate,
                    rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
                .CreateLogger();
            try
            {
                Log.Information("====================================================================");
                Log.Information($"Application Starts. Version: {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");
                Log.Information($"Application Directory: {AppDomain.CurrentDomain.BaseDirectory}");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application terminated unexpectedly");
            }
            finally
            {
                Log.Information("====================================================================\r\n");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddHostedService<Worker>();
                    var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>()
                    .UseSqlServer(configuration.GetSection("AppSettings:DefaultConnection").Value);
                    services.AddScoped(s => new RepositoryContext(optionsBuilder.Options));
                    services.AddScoped<IWrappedScopedRepository, WrappedScopedRepository>();
                    services.AddScoped<IDriverService, DriverService>();
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
                });
    }
}
