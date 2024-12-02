using Outsera.MovieListReader.Borders.Models;

namespace Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses
{
    public class GetAllMoviesStudioResponse
    {
        public string Name { get; set; }

        public static GetAllMoviesStudioResponse FromStudio(Studio studio) => new() { Name = studio.Name };
    }
}
