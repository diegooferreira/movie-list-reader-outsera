using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Outsera.MovieListReader.Borders.Services.CsvDataImport;
using Outsera.MovieListReader.Borders.Services.Dtos;
using Outsera.MovieListReader.Borders.Shared.Constants;
using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses;
using System.Net.Http.Json;

namespace Outsera.MovieListReader.Tests.Integration.GoldenRaspberryAwards
{
    public class EnsureDataIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public EnsureDataIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAwardsRange_WhenResponseReturnRecords_ReturnsOk()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var httpResponse = await client.GetAsync("/api/GoldenRaspberryAwards/winners/min-max-range");

            // Assert
            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var response = await httpResponse.Content.ReadFromJsonAsync<GetWinnersMinMaxRangeResponse>();

            response.Should().NotBeNull();
            response!.Min.Should().NotBeEmpty();
            response!.Max.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllMovies_CompareWithSourceCsvFile_ReturnsOk()
        {
            // Arrange
            ICsvReaderService csvReaderService;

            using (var scope = _factory.Services.CreateScope())
                csvReaderService = scope.ServiceProvider.GetRequiredService<ICsvReaderService>();

            var config = _factory.Services.GetRequiredService<IConfiguration>();
            var filePath = config["MovieListCsvPath"];

            var moviesCsvData = csvReaderService.ReadCsv<GoldenRaspberryAwardsLineContent>(filePath);

            var expectedResult = ToGetAllMoviesResponse(moviesCsvData);

            var client = _factory.CreateClient();

            // Act
            var httpResponse = await client.GetAsync("/api/GoldenRaspberryAwards/movies");

            // Assert
            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var response = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<GetAllMoviesResponse>>();

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResult);
        }

        private IEnumerable<GetAllMoviesResponse> ToGetAllMoviesResponse(IEnumerable<GoldenRaspberryAwardsLineContent> csvData) =>
            csvData.Select(data => new GetAllMoviesResponse()
            {
                Year = int.Parse(data.Year),
                Title = data.Title,
                Winner = NormalizeDataConstants.WinnerAsTrueValues.Contains(data.Winner),
                Producers = HandleLineWithMultipleInformation(data.Producers).Select(p => new GetAllMoviesProducerResponse { Name = p }),
                Studios = HandleLineWithMultipleInformation(data.Studios).Select(p => new GetAllMoviesStudioResponse { Name = p })
            });

        private static IEnumerable<string> HandleLineWithMultipleInformation(string lineContent) =>
            lineContent.Split(new[] { ",", " and " }, StringSplitOptions.RemoveEmptyEntries).Select(content => content.Trim()).Distinct();
    }
}
