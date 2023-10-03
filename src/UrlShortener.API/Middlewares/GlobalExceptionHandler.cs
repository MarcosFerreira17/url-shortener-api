using System.Net;
using UrlShortener.Domain.ErrorHandler;

namespace UrlShortener.API.Middlewares;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
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
