using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Error;

namespace KeyNekretnine.Presentation.Infrastructure
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;

        protected ApiController(ISender sender) => Sender = sender;

        protected IActionResult HandleFailure(Result result)
        {
            switch (result)
            {
                case { IsSuccess: true }:
                    throw new InvalidOperationException();

                case IValidationResult validationResult:
                    return BadRequest(CreateProblemDetails(
                        "Validation Error",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors));
                case IMultipleErrorsResult multipleErrorsResult:
                    return BadRequest(CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        multipleErrorsResult.Errors));

                default:
                    if (result.Error.Code.Contains("NotFound"))
                    {
                        return NotFound(result.Error);
                    }
                    return BadRequest(result.Error);
            }
        }

        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            Error[] errors = null) =>
            new()
            {
                Title = title,
                Type = error.Code,
                Detail = error.Message,
                Status = status,
                Extensions = { { nameof(errors), errors } }
            };
    }
}
