using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Infrastructure.ExceptionHandling.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment environment;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            this.next = next;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                var responseObj = HandleException(e, out var statusCode);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.Body.WriteAsync(
                    JsonSerializer.SerializeToUtf8Bytes(
                        responseObj, new JsonSerializerOptions { IgnoreNullValues = true }));
            }
        }

        private object HandleException(Exception e, out int statusCode)
        {
            var errorResponse = new ExceptionModel
            {
                Message = e.Message
            };

            if (environment.IsDevelopment())
            {
                errorResponse.StackTrace = e.StackTrace;
            }

            statusCode = StatusCodes.Status500InternalServerError;

            if (e is NotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
            }

            if (e is Application.Infrastructure.Exceptions.ValidationException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }

            return errorResponse;
        }
    }
}
