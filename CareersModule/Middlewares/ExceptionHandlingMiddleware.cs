using Application.Exceptions;
using Application.Responses;
using System.Net;
using System.Text.Json;

namespace Presentation.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message, details) = exception switch
            {
                NotFoundException notFoundEx => (HttpStatusCode.NotFound, notFoundEx.Message, (string?)null),
                ArgumentNullException argNullEx => (HttpStatusCode.BadRequest, "Invalid input provided.", argNullEx.Message),
                ArgumentException argEx => (HttpStatusCode.BadRequest, "Invalid argument provided.", argEx.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Access denied.", (string?)null),
                _ => (HttpStatusCode.InternalServerError, "An internal server error occurred.", exception.Message)
            };

            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = message,
                Details = details,
                Timestamp = DateTime.UtcNow
            };

            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
