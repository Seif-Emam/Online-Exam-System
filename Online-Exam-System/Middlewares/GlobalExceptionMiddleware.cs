using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Online_Exam_System.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.ContentType = "application/json";
                var statusCode = HttpStatusCode.InternalServerError;
                object errorResponse;

                switch (ex)
                {
                    case ValidationException validationEx:
                        statusCode = HttpStatusCode.BadRequest;
                        errorResponse = new
                        {
                            statusCode = (int)statusCode,
                            message = "Validation failed.",
                            errors = new[] { new { PropertyName = "N/A", ErrorMessage = validationEx.Message } }
                        };
                        break;

                    case BadHttpRequestException badReqEx:
                        statusCode = HttpStatusCode.BadRequest;
                        errorResponse = new
                        {
                            statusCode = (int)statusCode,
                            message = badReqEx.Message
                        };
                        break;

                    case KeyNotFoundException keyNotFoundEx:
                        statusCode = HttpStatusCode.NotFound;
                        errorResponse = new
                        {
                            statusCode = (int)statusCode,
                            message = keyNotFoundEx.Message
                        };
                        break;

                    case UnauthorizedAccessException:
                        statusCode = HttpStatusCode.Unauthorized;
                        errorResponse = new
                        {
                            statusCode = (int)statusCode,
                            message = "Unauthorized access."
                        };
                        break;

                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        errorResponse = new
                        {
                            statusCode = (int)statusCode,
                            message = "An unexpected error occurred.",
                            details = _env.IsDevelopment() ? ex.Message : null
                        };
                        break;
                }

                context.Response.StatusCode = (int)statusCode;

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(errorResponse, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
