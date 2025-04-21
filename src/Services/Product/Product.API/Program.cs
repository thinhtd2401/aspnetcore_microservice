
using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
try
{
    
    builder.Services.AddSerilogger(builder.Environment, builder.Configuration, AppDomain.CurrentDomain);
    
    builder.Host.UseSerilog();
    Log.Information("Starting Product API up");
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    app.UseInfrastructure();
    app.MigrateDatabase<ProductContext>((context, services) =>
    {
        var logger = services.GetService<ILogger<ProductContextSeed>>();
        if (logger != null) ProductContextSeed.SeedAsync(context, logger).Wait();
    }).Run();
    
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
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

        
