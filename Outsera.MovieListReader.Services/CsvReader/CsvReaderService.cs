using CsvHelper.Configuration;
using Outsera.MovieListReader.Borders.Services.CsvDataImport;
using System.Globalization;

namespace Outsera.MovieListReader.Services.CsvReader
{
    public class CsvReaderService : ICsvReaderService
    {
        public IEnumerable<T> ReadCsv<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File path {filePath} not found.");

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                Delimiter = ";"
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvHelper.CsvReader(reader, csvConfig);

            return csv.GetRecords<T>().ToList();
        }
    }
}