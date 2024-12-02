namespace Outsera.MovieListReader.Borders.Services.GoldenRaspberryAwards
{
    public interface IGoldenRaspberryAwardsService
    {
        Task ImportDataFromCSV(string filePath);
    }
}
