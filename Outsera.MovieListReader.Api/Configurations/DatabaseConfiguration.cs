using Microsoft.EntityFrameworkCore;
using Outsera.MovieListReader.Repository.Infra.Context;

namespace Outsera.MovieListReader.Api.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseInMemoryDatabase("OutseraMovieListReaderDB"));
        }
    }
}
