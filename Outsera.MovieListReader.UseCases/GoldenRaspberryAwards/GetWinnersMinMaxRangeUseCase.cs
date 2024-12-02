using Outsera.MovieListReader.Borders.Models;
using Outsera.MovieListReader.Borders.Repositories.GoldenRaspberryAwards;
using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards;
using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses;

namespace Outsera.MovieListReader.UseCases.GoldenRaspberryAwards
{
    public class GetWinnersMinMaxRangeUseCase : IGetWinnersMinMaxRangeUseCase
    {
        private readonly IGoldenRaspberryAwardsRepository _goldenRaspberryAwardsRepository;

        public GetWinnersMinMaxRangeUseCase(IGoldenRaspberryAwardsRepository goldenRaspberryAwardsRepository)
        {
            _goldenRaspberryAwardsRepository = goldenRaspberryAwardsRepository;
        }

        public async Task<GetWinnersMinMaxRangeResponse> Execute()
        {
            var movies = await _goldenRaspberryAwardsRepository.GetAllMovies();

            IEnumerable<KeyValuePair<string, List<int>>> awardWinnerProducers = GetAwardWinners(movies);

            var intervals = GetWinnerIntervals(awardWinnerProducers);

            var largestYearInterval = intervals.Max(i => i.MaxInterval);
            var smallestYearInterval = intervals.Min(i => i.MinInterval);

            var largestIntervals = intervals
                .Where(i => i.MaxInterval == largestYearInterval);

            var smallestIntervals = intervals
                .Where(i => i.MinInterval == smallestYearInterval);

            GetWinnersMinMaxRangeResponse response = BuildResponse(largestIntervals, smallestIntervals);

            return response;
        }

        private static GetWinnersMinMaxRangeResponse BuildResponse(IEnumerable<ProducerInterval> largestIntervals, IEnumerable<ProducerInterval> smallestIntervals) =>
            new GetWinnersMinMaxRangeResponse(
                    smallestIntervals.SelectMany(m =>
                        m.Intervals
                        .OrderBy(mm => mm.PrevisousWinYear)
                        .Select(mm =>
                        new GetWinnersMinMaxRangeResponseDetailResponse
                        {
                            Producer = m.Producer,
                            Interval = mm.IntervalYears,
                            PreviousWin = mm.PrevisousWinYear,
                            FollowingWin = mm.FollowingWinYear
                        })),
                    largestIntervals.SelectMany(m =>
                        m.Intervals
                        .OrderBy(mm => mm.PrevisousWinYear)
                        .Select(mm =>
                        new GetWinnersMinMaxRangeResponseDetailResponse
                        {
                            Producer = m.Producer,
                            Interval = mm.IntervalYears,
                            PreviousWin = mm.PrevisousWinYear,
                            FollowingWin = mm.FollowingWinYear
                        })));


        private static IEnumerable<ProducerInterval> GetWinnerIntervals(IEnumerable<KeyValuePair<string, List<int>>> awardWinnerProducers) =>
            awardWinnerProducers
                .Select(awp =>
                    {
                        var years = awp.Value.Select(year => year).OrderBy(year => year);

                        var winnersIntervals = years.Zip(years.Skip(1), (previsousMovie, followingMovie) =>
                            new Interval
                            {
                                IntervalYears = followingMovie - previsousMovie,
                                PrevisousWinYear = previsousMovie,
                                FollowingWinYear = followingMovie
                            });

                        var largestWinnersInterval = winnersIntervals.Max(d => d.IntervalYears);
                        var smallestWinnersInterval = winnersIntervals.Min(d => d.IntervalYears);

                        return new ProducerInterval
                        {
                            Producer = awp.Key,
                            MaxInterval = largestWinnersInterval,
                            MinInterval = smallestWinnersInterval,
                            Intervals = winnersIntervals
                        };
                    });

        private static IEnumerable<KeyValuePair<string, List<int>>> GetAwardWinners(IEnumerable<Movie> movies) =>
             movies
                .Where(m => m.Winner)
                .SelectMany(movie => movie.Producers.Select(producer => new { Producer = producer.Name, movie.Year }))
                .GroupBy(p => p.Producer)
                .Where(g => g.Count() > 1)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Year).OrderBy(y => y).ToList());
    }

    internal record Interval
    {
        public int IntervalYears { get; init; }
        public int PrevisousWinYear { get; init; }
        public int FollowingWinYear { get; init; }
    }

    internal record ProducerInterval
    {
        public string Producer { get; init; }
        public int MaxInterval { get; init; }
        public int MinInterval { get; init; }
        public IEnumerable<Interval> Intervals { get; init; }
    }
}
