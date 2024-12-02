using Microsoft.AspNetCore.Mvc;
using Outsera.MovieListReader.Api.Models;
using Outsera.MovieListReader.Borders.Shared;
using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards;
using Outsera.MovieListReader.Borders.UseCases.GoldenRaspberryAwards.Responses;

namespace Outsera.MovieListReader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldenRaspberryAwardsController(IActionResultConverter actionResultConverter) : ControllerBase
    {
        [HttpGet("winners/min-max-range")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetWinnersMinMaxRangeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> GetAwardsRange([FromServices] IGetWinnersMinMaxRangeUseCase getAwardsRangeUseCase) =>
            await actionResultConverter.Convert(getAwardsRangeUseCase.Execute);

        [HttpGet("movies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllMoviesResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<IActionResult> GetAll([FromServices] IGetAllMoviesUseCase getAllMoviesUseCase) =>
            await actionResultConverter.Convert(getAllMoviesUseCase.Execute);
    }
}
