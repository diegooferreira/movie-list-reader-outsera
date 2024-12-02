using Outsera.MovieListReader.Borders.Models;

namespace Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses
{
    public record GetAllMoviesResponse
    {
        public int Year { get; set; }
        public string Title { get; set; }
        public bool Winner { get; set; }
        public IEnumerable<GetAllMoviesProducerResponse> Producers { get; set; }
        public IEnumerable<GetAllMoviesStudioResponse> Studios { get; set; }

        public static GetAllMoviesResponse FromMovie(Movie movie) =>
            new()
            {
                Year = movie.Year,
                Title = movie.Title,
                Winner = movie.Winner,
                Producers = movie.Producers.Select(GetAllMoviesProducerResponse.FromProducer),
                Studios = movie.Studios.Select(GetAllMoviesStudioResponse.FromStudio),
            };
    }
}
