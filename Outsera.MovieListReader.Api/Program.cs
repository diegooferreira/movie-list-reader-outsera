using Outsera.MovieListReader.Api.Configurations;
using Outsera.MovieListReader.Api.Models;
using Outsera.MovieListReader.Borders.Shared;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebApplication(args);
    }

    public static void CreateWebApplication(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.ConfigureValidators();
        builder.ConfigureUseCases();
        builder.ConfigureServices();
        builder.ConfigureRepositories();
        builder.ConfigureDatabase();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IActionResultConverter, ActionResultConverter>();

        var app = builder.Build();

        IEnumerable<ErrorMessage> initializationError = app.ImportData(builder);

        if (initializationError.Any())
        {
            app.Map("/", async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(JsonSerializer.Serialize(initializationError));
            });

            app.Run();

            return;
        }

        app.ConfigureSwagger();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}