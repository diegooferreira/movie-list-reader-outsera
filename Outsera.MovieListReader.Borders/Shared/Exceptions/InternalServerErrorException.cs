namespace Outsera.MovieListReader.Borders.Shared.Exceptions
{
    public class InternalServerErrorException(ErrorMessage errorMessage) : Exception(errorMessage.Message)
    {
        public ErrorMessage ErrorMessage { get; private set; } = errorMessage;
    }
}
