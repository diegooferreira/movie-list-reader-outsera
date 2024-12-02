using Outsera.MovieListReader.Borders.Services.Dtos;
using Outsera.MovieListReader.Borders.Shared.Constants;

namespace Outsera.MovieListReader.Borders.Models
{
    public class Movie : BaseModel
    {
        private readonly List<Studio> _studios = [];
        private readonly List<Producer> _producers = [];

        public int Year { get; init; }
        public string Title { get; init; }
        public IReadOnlyCollection<Studio> Studios => _studios.AsReadOnly();
        public IReadOnlyCollection<Producer> Producers => _producers.AsReadOnly();
        public bool Winner { get; init; }

        public Movie()
        {
            
        }

        public Movie(GoldenRaspberryAwardsLineContent lineContent)
        {
            Year = int.Parse(lineContent.Year);
            Title = lineContent.Title;
            Winner = NormalizeDataConstants.WinnerAsTrueValues.Contains(lineContent.Winner);

            _producers.AddRange(HandleLineWithMultipleInformation(lineContent.Producers).Select(p => new Producer { Name = p }));
            _studios.AddRange(HandleLineWithMultipleInformation(lineContent.Studios).Select(p => new Studio { Name = p }));
        }

        private static IEnumerable<string> HandleLineWithMultipleInformation(string lineContent) =>
            lineContent.Split(new[] { ",", " and " }, StringSplitOptions.RemoveEmptyEntries).Select(content => content.Trim()).Distinct();
    }
}
