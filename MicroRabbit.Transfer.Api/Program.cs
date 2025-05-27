using MicroRabbit.Transfer.Api;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Manually instantiate Startup
        var startup = new Startup(builder.Configuration);

        // Call ConfigureServices
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        // Call Configure
        startup.Configure(app, builder.Environment);

        app.Run();
    }
}
