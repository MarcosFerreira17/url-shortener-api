using System.Security.Cryptography;
using MongoDB.Bson.Serialization.Attributes;

namespace UrlShortenerAPI.Data;
public class Url
{
    public Url() { }
    public Url(string originalUrl)
    {
        OriginalUrl = originalUrl;
    }

    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("original_url")]
    public string OriginalUrl { get; set; }
    [BsonElement("hash")]
    public string Hash { get; set; }

    public static string GenerateHash(string originalUrl)
    {
        byte[] salt = new byte[8];

        var pbkdf2 = new Rfc2898DeriveBytes(originalUrl, salt, 1000);

        byte[] hash = pbkdf2.GetBytes(8);
        byte[] hashBytes = new byte[6];

        Array.Copy(salt, 0, hashBytes, 0, 2);
        Array.Copy(hash, 0, hashBytes, 2, 4);

        return Convert.ToBase64String(hashBytes);
    }

    public static bool CheckUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return false;

        Uri uriResult;
        bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        return result;
    }
}
