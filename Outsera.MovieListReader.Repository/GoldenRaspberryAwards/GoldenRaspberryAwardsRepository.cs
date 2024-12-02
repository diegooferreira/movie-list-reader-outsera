using Microsoft.EntityFrameworkCore;
using Outsera.MovieListReader.Borders.Models;
using Outsera.MovieListReader.Borders.Repositories.GoldenRaspberryAwards;
using Outsera.MovieListReader.Repository.Infra.Context;

namespace Outsera.MovieListReader.Repository.GoldenRaspberryAwards
{
    public class GoldenRaspberryAwardsRepository : IGoldenRaspberryAwardsRepository
    {
        private readonly ApplicationDbContext _context;

        public GoldenRaspberryAwardsRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task ClearMovies()
        {
            _context.Movies.RemoveRange(_context.Movies);
            await _context.SaveChangesAsync();
        }

        public async Task AddMovies(IEnumerable<Movie> movies)
        {
            await _context.Movies.AddRangeAsync(movies);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies.Include(m => m.Producers).Include(m => m.Studios).ToListAsync();
        }
    }
}
