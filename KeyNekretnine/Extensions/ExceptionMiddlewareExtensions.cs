using Contracts;
using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace KeyNekretnine.Extensions;
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        ValidationAppException => StatusCodes.Status422UnprocessableEntity,
                        AuthenticationException => StatusCodes.Status409Conflict,
                        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    logger.LogError($"Something went wrong: {contextFeature.Error}");

                    if (contextFeature.Error is ValidationAppException validationAppException)
                    {
                        await context.Response
                        .WriteAsync(JsonSerializer.Serialize(new
                        {
                            validationAppException.Errors
                        }));
                    }
                    else if (contextFeature.Error is AuthenticationException authenticationException)
                    {
                        await context.Response
                        .WriteAsync(JsonSerializer.Serialize(new
                        {
                            authenticationException.Errors
                        }));
                    }
                    else
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                }
            });
        });
    }
}