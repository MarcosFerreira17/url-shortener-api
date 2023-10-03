using System.Text.Json;
using System.Text.Json.Serialization;

namespace UrlShortener.Domain.ErrorHandler;

public class ErrorResponse
{
    [JsonIgnore]
    public int HttpStatusCode { get; set; }
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public static ErrorResponse InternalErrror()
    {
        return new ErrorResponse
        {
            HttpStatusCode = 500,
            Code = "InternalError",
            Message = "An internal error has occurred. Please try again later."
        };
    }

    public static ErrorResponse NotFound(string message)
    {
        return new ErrorResponse
        {
            HttpStatusCode = 404,
            Code = "NotFound",
            Message = message
        };
    }

    public static ErrorResponse BadRequest(string message)
    {
        return new ErrorResponse
        {
            HttpStatusCode = 400,
            Code = "BadRequest",
            Message = message
        };
    }

    public static ErrorResponse Unauthorized(string message)
    {
        return new ErrorResponse
        {
            HttpStatusCode = 401,
            Code = "Unauthorized",
            Message = message
        };
    }

    public static ErrorResponse Forbidden(string message)
    {
        return new ErrorResponse
        {
            HttpStatusCode = 403,
            Code = "Forbidden",
            Message = message
        };
    }

    public static ErrorResponse Conflict(string message)
    {
        return new ErrorResponse
        {
            HttpStatusCode = 409,
            Code = "Conflict",
            Message = message
        };
    }

}
