using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging;

public static class Serilogger
{
    public static void AddSerilogger(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
    {
        var applicationName = environment.ApplicationName.ToLower().Replace(".", "-");
        var environmentName = environment.EnvironmentName;
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level}] {" +
                "SourceContext" +
                "}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Environment", environmentName)
            .Enrich.WithProperty("Application", applicationName)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}