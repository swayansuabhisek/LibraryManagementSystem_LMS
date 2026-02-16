using System.Net;
using LMS.Shared.Response;

namespace LMS.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            var response = new APIResponse<object>
            {
                IsSuccess = false
            };
            switch (ex)
            {
                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = $"Invalid Request: {ex.Message}";
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = $"An unexpected error occured:  {ex.Message}";
                    break;
            }
            
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}