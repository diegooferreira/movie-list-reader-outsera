﻿using Outsera.MovieListReader.Borders.Shared.UseCase;
using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses;

namespace Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards
{
    public interface IGetAllMoviesUseCase : IUseCaseBase<IEnumerable<GetAllMoviesResponse>>
    {
    }
}