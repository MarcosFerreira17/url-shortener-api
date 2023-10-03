using System;
using System.Net;

namespace UrlShortener.Domain.ErrorHandler;
public class HttpStatusCodeException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public string Code { get; set; }
    public string ContentType { get; set; } = @"text/plain";

    public HttpStatusCodeException(HttpStatusCode httpStatusCode, string message) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCodeException(HttpStatusCode httpStatusCode, string code, string message) : base(message)
    {
        HttpStatusCode = httpStatusCode;
        Code = code;
    }

    public HttpStatusCodeException(HttpStatusCode httpStatusCode, string code, string message, string contentType) : base(message)
    {
        HttpStatusCode = httpStatusCode;
        Code = code;
        ContentType = contentType;
    }

    public HttpStatusCodeException(HttpStatusCode httpStatusCode, string code, string message, Exception inner) : base(message, inner)
    {
        HttpStatusCode = httpStatusCode;
        Code = code;
    }

    public HttpStatusCodeException(HttpStatusCode httpStatusCode, string code, string message, Exception inner, string contentType) : base(message, inner)
    {
        HttpStatusCode = httpStatusCode;
        Code = code;
        ContentType = contentType;
    }

    public HttpStatusCodeException(HttpStatusCode httpStatusCode, string message, Exception inner) : base(message, inner)
    {
        HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCodeException(HttpStatusCode httpStatusCode, string message, Exception inner, string contentType) : base(message, inner)
    {
        HttpStatusCode = httpStatusCode;
        ContentType = contentType;
    }
}
