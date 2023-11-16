using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.ErrorHandler;

namespace UrlShortener.Application.Middlewares;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(ex, "An error ocurred while trying to access database.");
            await HandleExceptionAsync(context, ex);
        }
        catch (HttpStatusCodeException ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
    {
        var result = new ErrorResponse
        {
            Code = exception.Code,
            Message = exception.Message
        };

        context.Response.StatusCode = (int)exception.HttpStatusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(result.ToString());
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = new ErrorResponse
        {
            Code = "Internal Server Errror",
            Message = exception.Message
        };

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(result.ToString());
    }

}
