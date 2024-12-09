using System.Net;
using System.Text.Json;
using Tournaments.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Tournament.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Instance = context.Request.Path
            };

            switch (exception)
            {
                case NotFoundException notFoundException:
                    problemDetails.Title = "Resource Not Found";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Detail = notFoundException.Message;
                    break;

                case BusinessRuleViolationException businessException:
                    problemDetails.Title = "Business Rule Violation";
                    problemDetails.Status = (int)HttpStatusCode.UnprocessableEntity;
                    problemDetails.Detail = businessException.Message;
                    break;

                default:
                    problemDetails.Title = "An error occurred";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Detail = "Internal server error occurred.";
                    break;
            }

            context.Response.StatusCode = problemDetails.Status.Value;

            var json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        }
    }
}