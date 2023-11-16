using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UrlShortener.Domain.Common.CustomExceptions;

namespace UrlShortener.Domain.Url.Entities;
public class ShortUrl
{
    [JsonConstructor]
    public ShortUrl(string id, DateTime createdAt, string longUrlText, string text)
    {
        Id = id;
        CreatedAt = createdAt;
        LongUrlText = longUrlText;
        Text = text;
    }
    public ShortUrl(DateTime createdAt, string longUrlText)
    {
        CreatedAt = createdAt;
        LongUrlText = longUrlText;
        Text = CreateShortUrl(longUrlText);
    }

    [BsonId]
    [BsonElement("ShortUrlId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    [BsonElement("ShortUrlText")]
    public string LongUrlText { get; private set; }
    public string Text { get; private set; }

    private string CreateShortUrl(string longUrlText)
    {
        if (!IsValidUrl(longUrlText))
            throw new DomainLogicException(ErrorConstants.UrlIdIsNotValidUrl);

        return GenerateHash(longUrlText);
    }

    private static string GenerateHash(string originalUrl)
    {
        if (!IsValidUrl(originalUrl))
            throw new DomainLogicException(ErrorConstants.UrlIdIsNotValidUrl);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(originalUrl);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // Converte os primeiros 8 bytes do hash para uma sequência de caracteres base64
            string shortUrl = Convert.ToBase64String(hashBytes, 0, 8)
                .Replace("/", "_") // Substitui caracteres não permitidos em URLs
                .Replace("+", "-");

            return shortUrl;
        };
    }

    private static bool IsValidUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return false;

        return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
