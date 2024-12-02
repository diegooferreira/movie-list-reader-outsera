namespace Outsera.MovieListReader.Borders.Shared;

public static class ErrorMessages
{
    public static readonly ErrorMessage ErrorCommunicatingWithIdentity = new(ErrorCodes.InternalServerError, "Error communicating with Identity.");
    public static readonly ErrorMessage UnexpectedError = new(ErrorCodes.InternalServerError, "Unexpected error on execute service.");

    public static readonly ErrorMessage FileDataValidationError = new(ErrorCodes.ValidationError, "Invalid Data in imported file");
    public static readonly ErrorMessage CsvFileIsEmptyError = new(ErrorCodes.BadRequest, "Csv file is empty");
}
