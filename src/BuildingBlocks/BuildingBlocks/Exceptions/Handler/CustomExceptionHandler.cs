﻿
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {message}, time of occurance: {time}",  exception.Message, DateTime.UtcNow);

            (string Details, string Title, int StatusCode) error = exception switch
            {
                InternalServerException => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError),
                BadRequestException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
                NotFoundException => (exception.Message, exception.GetType().Name, StatusCodes.Status404NotFound),
                ValidationException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
                _ => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
                
            };

            var problemDetails = new ProblemDetails
            {
                Title = error.Title,
                Status = error.StatusCode,
                Detail = error.Details,
                Type = exception.GetType().Name,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            if(exception is ValidationException validationException)
            {
                var errors = validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage });
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;

        }
    }
}
