using FluentValidation;
using Outsera.MovieListReader.Borders.Services.Dtos;
using Outsera.MovieListReader.Borders.Shared.Constants;

namespace Outsera.MovieListReader.Borders.Services.Validators
{
    public class GoldenRaspberryAwardsLineContentValidator : AbstractValidator<GoldenRaspberryAwardsLineContent>
    {
        public GoldenRaspberryAwardsLineContentValidator()
        {
            RuleFor(g => g.Year).NotEmpty();
            RuleFor(g => g.Title).NotEmpty();
            RuleFor(g => g.Studios).NotEmpty();
            RuleFor(g => g.Producers).NotEmpty();

            When(g => !string.IsNullOrWhiteSpace(g.Winner), () =>
            {
                RuleFor(w => w.Winner).Must(m => CheckWinnerValidValue(m.ToLower()));
            });
        }

        private bool CheckWinnerValidValue(string winnnerCurrentValue) => NormalizeDataConstants.WinnerValidValues.Contains(winnnerCurrentValue);
    }
}
