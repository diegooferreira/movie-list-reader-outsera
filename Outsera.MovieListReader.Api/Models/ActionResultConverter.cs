using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Outsera.MovieListReader.Borders;
using Outsera.MovieListReader.Borders.Shared;
using Outsera.MovieListReader.Borders.Shared.Exceptions;
using System.Net;

namespace Outsera.MovieListReader.Api.Models
{
    public interface IActionResultConverter
    {
        Task<IActionResult> Convert<T>(Func<Task<T>> action);
    }

    public class ActionResultConverter(IHttpContextAccessor accessor) : IActionResultConverter
    {
        private readonly string path = accessor?.HttpContext?.Request.Path.Value ?? string.Empty;

        public async Task<IActionResult> Convert<T>(Func<Task<T>> action)
        {
            try
            {
                var response = await action();

                if (response == null)
                    return BuildError(new[] { new ErrorMessage(ErrorCodes.InternalServerError, "ActionResultConverter Error") }, HttpStatusCode.InternalServerError);

                return BuildSuccessResult(response, HttpStatusCode.OK);
            }
            catch (BadRequestException bre)
            {
                return BuildError(bre, HttpStatusCode.BadRequest);
            }
            catch (NotFoundException nfe)
            {
                return BuildError(nfe, HttpStatusCode.NotFound);
            }
            catch (ValidationException ve)
            {
                return BuildError(ve, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return BuildError(e, HttpStatusCode.InternalServerError);
            }
        }


        private IActionResult BuildSuccessResult(object data, HttpStatusCode status) => status switch
        {
            _ => new OkObjectResult(data),
        };

        private static ObjectResult BuildError(object data, HttpStatusCode status) => new(data) { StatusCode = (int)status };
    }
}
