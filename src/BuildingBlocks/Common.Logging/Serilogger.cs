using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging;

public static class Serilogger
{
    public static void AddSerilogger(this IServiceCollection services, IHostEnvironment environment,
        IConfiguration configuration, AppDomain appDomain)
    {
        try
        {
            if (appDomain.IsRunningFromEf()) return;
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
        catch (Exception ex)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Warning(ex, "Failed to configure Serilog fully during startup.");
        }
    }

}

public static class AppDomainExtension
{
    public static bool IsRunningFromEf(this AppDomain appDomain)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Any(a => a.FullName!.StartsWith("Microsoft.EntityFrameworkCore.Design", StringComparison.OrdinalIgnoreCase));
    }
}