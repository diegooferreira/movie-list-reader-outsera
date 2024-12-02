using Outsera.MovieListReader.Borders.Repositories.GoldenRaspberryAwards;
using Outsera.MovieListReader.Repository.GoldenRaspberryAwards;

namespace Outsera.MovieListReader.Api.Configurations
{
    public static class RepositoryConfiguration
    {
        public static void ConfigureRepositories(this WebApplicationBuilder builder) 
        {
            builder.Services.AddScoped<IGoldenRaspberryAwardsRepository, GoldenRaspberryAwardsRepository>();
        }
    }
}
