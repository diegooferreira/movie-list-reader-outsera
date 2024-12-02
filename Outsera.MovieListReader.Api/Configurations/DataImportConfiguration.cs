using FluentValidation;
using Outsera.MovieListReader.Borders;
using Outsera.MovieListReader.Borders.Services.GoldenRaspberryAwards;
using Outsera.MovieListReader.Borders.Shared;
using Outsera.MovieListReader.Borders.Shared.Exceptions;

namespace Outsera.MovieListReader.Api.Configurations
{
    public static class DataImportConfiguration
    {
        public static IEnumerable<ErrorMessage> ImportData(this WebApplication app, WebApplicationBuilder builder)
        {
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var filePath = builder.Configuration["MovieListCsvPath"];

                    if (string.IsNullOrEmpty(filePath))
                        throw new InvalidOperationException("CSV File path is invalid");

                    var _goldenRaspberryAwardsService = scope.ServiceProvider.GetRequiredService<IGoldenRaspberryAwardsService>();
                    _goldenRaspberryAwardsService.ImportDataFromCSV(filePath).GetAwaiter().GetResult();
                }

                return [];
            }
            catch (Exception ex)
            {
                if (ex is AggregateException && ex.InnerException is BadRequestException)
                {
                    return [((BadRequestException)ex.InnerException).ErrorMessage];
                }
                else if (ex is AggregateException && ex.InnerException is ValidationException)
                {
                    return ((ValidationException)ex.InnerException).Errors.Select(e => new ErrorMessage(e.ErrorCode, e.ErrorMessage));
                }
                else
                {
                    return [new ErrorMessage(ErrorCodes.InternalServerError, ex.Message)];
                }
            }
        }
    }
}
