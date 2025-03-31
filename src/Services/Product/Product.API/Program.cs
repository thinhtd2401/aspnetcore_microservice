
using Common.Logging;
using Product.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
try
{
    builder.Services.AddSerilogger(builder.Environment, builder.Configuration);
    builder.Host.UseSerilog();
    Log.Information("Start Product API up");
    
    builder.Services.AddInfrastructure(builder.Configuration);
    
    var app = builder.Build();
    app.UseInfrastructure();
    app.MapControllers();
    app.Run();
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
        
