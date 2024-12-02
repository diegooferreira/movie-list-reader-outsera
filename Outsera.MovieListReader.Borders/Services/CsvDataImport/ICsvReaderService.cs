namespace Outsera.MovieListReader.Borders.Services.CsvDataImport
{
    public interface ICsvReaderService
    {
        IEnumerable<T> ReadCsv<T>(string filePath) where T : class;
    }
}
