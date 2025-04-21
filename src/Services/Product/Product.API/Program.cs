
using Common.Logging;
using Product.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
try
{
    if (!IsRunningFromEF())
    {
        builder.Services.AddSerilogger(builder.Environment, builder.Configuration);
    }
    builder.Host.UseSerilog();
    Log.Information("Starting Product API up");

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

// Helper method to detect if we're running `dotnet ef`
static bool IsRunningFromEF()
{
    return AppDomain.CurrentDomain.GetAssemblies()
        .Any(a => a.FullName!.StartsWith("Microsoft.EntityFrameworkCore.Design", StringComparison.OrdinalIgnoreCase));
}


        
