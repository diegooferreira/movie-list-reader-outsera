using Outsera.MovieListReader.Borders.Repositories.GoldenRaspberryAwards;
using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards;
using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses;

namespace Outsera.MovieListReader.UseCases.GoldenRaspberryAwards
{
    public class GetAllMoviesUseCase : IGetAllMoviesUseCase
    {
        private readonly IGoldenRaspberryAwardsRepository _goldenRaspberryAwardsRepository;

        public GetAllMoviesUseCase(IGoldenRaspberryAwardsRepository goldenRaspberryAwardsRepository)
        {
            _goldenRaspberryAwardsRepository = goldenRaspberryAwardsRepository;
        }

        public async Task<IEnumerable<GetAllMoviesResponse>> Execute()
        {
            var movies = await _goldenRaspberryAwardsRepository.GetAllMovies();

            return movies.Select(GetAllMoviesResponse.FromMovie);
        }
    }
}
