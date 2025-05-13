using ForworkAcademy.ExceptionHandling;
using ForworkAcademy.Models.Errors;
using System.Net;

namespace ForworkAcademy.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;


        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                CourseNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = (int)statusCode;

            var response = new ErrorDetail()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                RequestId = context.TraceIdentifier,
                StackTrace = context.RequestServices.GetService<IWebHostEnvironment>().IsDevelopment() ? ex.StackTrace : null,
                Details = ex.InnerException?.Message
            };


            await context.Response.WriteAsync(response.ToString());

        }
    }
}
