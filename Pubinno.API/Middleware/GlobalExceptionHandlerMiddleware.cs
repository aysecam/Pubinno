using FluentValidation;
using Pubinno.Application.Interfaces.Services;

namespace Pubinno.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                ValidationException ex => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Validation failed",
                    Errors = ex.Errors.Select(e => e.ErrorMessage).ToList()
                },
                UnauthorizedAccessException => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Unauthorized"
                },
                KeyNotFoundException => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Resource not found"
                },
                _ => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An unexpected error occurred"
                }
            };

            var exceptionLogService = context.RequestServices.GetRequiredService<IExceptionLogService>();
            await exceptionLogService.LogAsync(
                context.Request.Path,
                context.Request.Method,
                exception.Message,
                exception.StackTrace,
                response.StatusCode);

            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public List<string>? Errors { get; set; }
    }
}
