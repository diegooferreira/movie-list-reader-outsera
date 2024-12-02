namespace Outsera.MovieListReader.Borders.Shared.UseCase
{
    public interface IUseCaseBase<TResponse>
    {
        Task<TResponse> Execute();
    }
}
