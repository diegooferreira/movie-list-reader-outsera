using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards;
using Outsera.MovieListReader.UseCases.GoldenRaspberryAwards;

namespace Outsera.MovieListReader.Api.Configurations
{
    public static class UseCaseConfiguration
    {
        public static void ConfigureUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IGetWinnersMinMaxRangeUseCase, GetWinnersMinMaxRangeUseCase>();
            builder.Services.AddScoped<IGetAllMoviesUseCase, GetAllMoviesUseCase>();
        }
    }
}
