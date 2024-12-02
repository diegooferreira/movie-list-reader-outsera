using FluentValidation;
using Outsera.MovieListReader.Borders.Models;
using Outsera.MovieListReader.Borders.Repositories.GoldenRaspberryAwards;
using Outsera.MovieListReader.Borders.Services.CsvDataImport;
using Outsera.MovieListReader.Borders.Services.Dtos;
using Outsera.MovieListReader.Borders.Services.GoldenRaspberryAwards;
using Outsera.MovieListReader.Borders.Shared;
using Outsera.MovieListReader.Borders.Shared.Exceptions;

namespace Outsera.MovieListReader.Services.GoldenRaspberryAwasrds
{
    public class GoldenRaspberryAwardsService : IGoldenRaspberryAwardsService
    {
        private readonly IValidator<GoldenRaspberryAwardsLineContent> _goldenRaspberryAwardsLineContentValidator;
        private readonly ICsvReaderService _csvReaderService;
        private readonly IGoldenRaspberryAwardsRepository _goldenRaspberryAwardsRepository;

        public GoldenRaspberryAwardsService(
            IValidator<GoldenRaspberryAwardsLineContent> goldenRaspberryAwardsLineContentValidator,
            ICsvReaderService csvReaderService,
            IGoldenRaspberryAwardsRepository goldenRaspberryAwardsRepository)
        {
            _goldenRaspberryAwardsLineContentValidator = goldenRaspberryAwardsLineContentValidator;
            _csvReaderService = csvReaderService;
            _goldenRaspberryAwardsRepository = goldenRaspberryAwardsRepository;
        }

        public async Task ImportDataFromCSV(string filePath)
        {
            IEnumerable<GoldenRaspberryAwardsLineContent> moviesCsvData = ProcessCsvData(filePath);

            var movies = moviesCsvData.Select(md => new Movie(md));

            if (!movies.Any())
                throw new BadRequestException(ErrorMessages.CsvFileIsEmptyError);

            await _goldenRaspberryAwardsRepository.ClearMovies();

            await _goldenRaspberryAwardsRepository.AddMovies(movies);
        }

        private IEnumerable<GoldenRaspberryAwardsLineContent> ProcessCsvData(string filePath)
        {
            var moviesCsvData = _csvReaderService.ReadCsv<GoldenRaspberryAwardsLineContent>(filePath);

            var validationResult = moviesCsvData.Select(_goldenRaspberryAwardsLineContentValidator.Validate);

            if (validationResult.Any(result => !result.IsValid))
                throw new BadRequestException(ErrorMessages.FileDataValidationError);

            return moviesCsvData;
        }
    }
}
