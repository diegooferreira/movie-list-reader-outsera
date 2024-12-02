using Outsera.MovieListReader.Borders.Services.CsvDataImport;
using Outsera.MovieListReader.Borders.Services.GoldenRaspberryAwards;
using Outsera.MovieListReader.Services.CsvReader;
using Outsera.MovieListReader.Services.GoldenRaspberryAwasrds;

namespace Outsera.MovieListReader.Api.Configurations
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICsvReaderService, CsvReaderService>();
            builder.Services.AddScoped<IGoldenRaspberryAwardsService, GoldenRaspberryAwardsService>();
        }
    }
}
