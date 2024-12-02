using Outsera.MovieListReader.Borders.Models;

namespace Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses
{
    public class GetAllMoviesProducerResponse
    {
        public string Name { get; set; }

        public static GetAllMoviesProducerResponse FromProducer(Producer producer) =>
            new() { Name = producer.Name };
    }
}
