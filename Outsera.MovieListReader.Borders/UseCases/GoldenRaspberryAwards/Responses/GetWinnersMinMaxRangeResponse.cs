namespace Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses
{
    public class GetWinnersMinMaxRangeResponse(IEnumerable<GetWinnersMinMaxRangeResponseDetailResponse> min, IEnumerable<GetWinnersMinMaxRangeResponseDetailResponse> max)
    {
        public IEnumerable<GetWinnersMinMaxRangeResponseDetailResponse> Min { get; init; } = min;

        public IEnumerable<GetWinnersMinMaxRangeResponseDetailResponse> Max { get; init; } = max;
    }
}
