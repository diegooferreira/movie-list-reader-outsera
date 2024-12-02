namespace Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses
{
    public class GetWinnersMinMaxRangeResponseDetailResponse
    {
        public string Producer { get; set; }

        public int Interval { get; set; }

        public int PreviousWin { get; set; }

        public int FollowingWin { get; set; }
    }
}
