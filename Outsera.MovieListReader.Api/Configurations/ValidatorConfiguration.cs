using FluentValidation;
using Outsera.MovieListReader.Borders.Services.Dtos;
using Outsera.MovieListReader.Borders.Services.Validators;

namespace Outsera.MovieListReader.Api.Configurations
{
    public static class ValidatorConfiguration
    {
        public static void ConfigureValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<GoldenRaspberryAwardsLineContent>, GoldenRaspberryAwardsLineContentValidator>();
        }
    }
}
