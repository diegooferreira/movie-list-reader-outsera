using Outsera.MovieListReader.Borders.Models;

namespace Outsera.MovieListReader.Borders.Repositories.GoldenRaspberryAwards
{
    public interface IGoldenRaspberryAwardsRepository
    {
        Task AddMovies(IEnumerable<Movie> movies);
        Task<IEnumerable<Movie>> GetAllMovies();
        Task ClearMovies();
    }
}
