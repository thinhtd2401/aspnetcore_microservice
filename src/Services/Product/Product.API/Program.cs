
using Common.Logging;
using Product.API.Extensions;
using Serilog;

namespace Product.API;

public static class Program
{
    public static void Main(string[] args)
    {
       
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppConfigurations();
            builder.Host.UseSerilog((context, configuration) =>
            {
                var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");
                var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

                configuration
                    .WriteTo.Debug()
                    .WriteTo.Console(outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level}] {" +
                        "SourceContext" +
                        "}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithProperty("Environment", environmentName)
                    .Enrich.WithProperty("Application", applicationName)
                    .ReadFrom.Configuration(context.Configuration);
            });
            Log.Information("Starting Product API 2");
            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();
            app.UseInfrastructure();
            app.MapControllers();
            app.Run();
            Log.Information("Runing Product API");
        }
        catch (Exception ex)
        {
            string type = ex.GetType().Name;
            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }

            Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
        }
        finally
        {
            Log.Information("Shut down Product API complete");
            Log.CloseAndFlush();
        }
        
    }
}