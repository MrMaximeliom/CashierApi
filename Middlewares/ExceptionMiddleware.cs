using CashierApi.Models;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
namespace CashierApi.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
                

            }
        }

        private async Task HandleExceptionAsync(HttpContext context , Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _hostEnvironment.IsDevelopment()
                ? new ExceptionHandlerResponse(context.Response.StatusCode,ex.Message , ex.StackTrace?.ToString())
                : new ExceptionHandlerResponse(context.Response.StatusCode,"Internal Server Error");

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);    
        
                
        }
    }
}
